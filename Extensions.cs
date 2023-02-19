using Extensions;
using HarmonyLib;
using Newtonsoft;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.Remoting.Messaging;
using System.Security.Policy;
using System.Speech;
using System.Speech.Recognition;
using System.Speech.Synthesis;
using System.Text;
using System.Threading.Tasks;
using ThunderRoad;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Rendering;
using UnityEngine.UI;
using UnityEngine.VFX;
using Action = System.Action;
using Methods = Extensions.Methods;
using Object = System.Object;
using Random = UnityEngine.Random;

namespace Extensions;
internal static class Methods {
	public static RagdollPart GetRagdollPart(this Creature creature, RagdollPart.Type ragdollPartType) =>
		creature.ragdoll.GetPart(ragdollPartType);

	public static void CompleteDismember(this Creature creature) {
		creature.Kill();
		creature.GetRagdollPart(RagdollPart.Type.Neck).TrySlice();
		creature.GetRagdollPart(RagdollPart.Type.LeftArm).TrySlice();
		creature.GetRagdollPart(RagdollPart.Type.RightArm).TrySlice();
		creature.GetRagdollPart(RagdollPart.Type.LeftLeg).TrySlice();
		creature.GetRagdollPart(RagdollPart.Type.RightLeg).TrySlice();
	}

	public static RagdollPart GetHeadPart(this Creature creature) => creature.ragdoll.headPart;

	public static Creature NearestCreature() =>
		Creature.allActive.Where(creature => !creature.isPlayer && !creature.isKilled)
			.OrderBy(creature => (Player.currentCreature.player.transform.position -
								  creature.transform.position).sqrMagnitude).FirstOrDefault();

	public static float DistanceBetweenCreatureAndPlayer(this Creature creature) =>
		(Player.currentCreature.player.transform.position - creature.transform.position).sqrMagnitude;

	public static PlayerControl.Hand ControlHand(this RagdollHand hand) => hand.playerHand.controlHand;

	public static bool EmptyHanded(this RagdollHand hand) =>
		hand.grabbedHandle is not null &&
		hand.caster.telekinesis.catchedHandle is not null &&
		!hand.caster.isFiring &&
		!hand.caster.isMerging &&
		!hand.caster.mana.mergeActive;

	public static bool GripPressed(this RagdollHand hand) => hand.ControlHand().gripPressed;

	public static bool TriggerPressed(this RagdollHand hand) => hand.ControlHand().usePressed;

	public static bool AlternateUsePressed(this RagdollHand hand) => hand.ControlHand().alternateUsePressed;

	public static float DistanceBetweenHands() => (GetHandSide(Side.Left).transform.position -
												   GetHandSide(Side.Right).transform.position).sqrMagnitude;

	public static RagdollHand GetHandSide(this Side side) => Player.currentCreature.GetHand(side);

	public static Vector3 PalmDirection(this RagdollHand hand) => hand.PalmDir;

	public static Vector3 DorsalHandPosition(this RagdollHand hand, float distance = -3.0f) =>
		-hand.Palm().transform.forward *
		-distance +
		hand.Palm().transform.position;

	public static Vector3 PalmarHandPosition(this RagdollHand hand, float distance = 3.0f) =>
		-hand.MagicTransform().forward *
		distance +
		hand.MagicTransform().position;

	public static Vector3 HandVelocity(this RagdollHand hand) =>
		Player.currentCreature.transform.rotation *
		hand.playerHand.controlHand.GetHandVelocity();

	public static float GetHandVelocityDirection(this RagdollHand hand, Vector3 direction) =>
		Vector3.Dot(hand.HandVelocity(), direction);

	public static Transform ThumbFingerTip(this RagdollHand hand) => hand.fingerThumb.tip;

	public static Transform IndexFingerTip(this RagdollHand hand) => hand.fingerIndex.tip;

	public static Vector3 AboveIndexTip(this RagdollHand hand, float distance = -2.0f) =>
			-hand.IndexFingerTip().transform.forward *
			-distance +
			hand.IndexFingerTip().transform.position;

	public static Transform MiddleFingerTip(this RagdollHand hand) => hand.fingerMiddle.tip;

	public static Transform RingFingerTip(this RagdollHand hand) => hand.fingerRing.tip;

	public static Transform PinkyFingerTip(this RagdollHand hand) => hand.fingerLittle.tip;

	public static Collider Palm(this RagdollHand hand) => hand.palmCollider;

	public static Transform HandTransform(this RagdollHand hand) => hand.transform;

	public static Vector3 HandPosition(this RagdollHand hand) => hand.transform.position;

	public static Quaternion HandRotation(this RagdollHand hand) => hand.transform.rotation;

	public static RagdollPart UpperArmPart(this RagdollHand hand) => hand.upperArmPart;

	public static RagdollPart LowerArmPart(this RagdollHand hand) => hand.lowerArmPart;

	public static Transform MagicTransform(this RagdollHand hand) => hand.caster.magic;

	public static void SnapRagdollPart(this Creature creature, RagdollPart.Type ragdollPartType) {
		creature.ragdoll.GetPart(ragdollPartType).DisableCharJointLimit();
		creature.ragdoll.GetPart(ragdollPartType).rb.useGravity = true;
	}

	public static void StopCreatureBrain(this Creature creature) {
		creature.brain.Stop();
		creature.ragdoll.SetState(Ragdoll.State.Destabilized);
	}

	public static void LoadCreatureBrain(this Creature creature) => creature.brain.Load(creature.brain.instance.id);

	public static void NoPhysicalKill(this Creature creature) {
		creature.ragdoll.SetState(Ragdoll.State.Inert);
		creature.Kill();
	}

	public static void EndCaster(this SpellCaster spellCaster) {
		spellCaster.isFiring = false;
		spellCaster.intensity = 0.0f;
		spellCaster.Fire(false);
	}
}

internal class Continuum {
	private Continuum _continuum;
	private Func<bool> _condition;
	private Action _action;
	private Type _type = Type.Start;

	private enum Type {
		Start,
		WaitFor,
		Do,
		End
	}

	public static Continuum Start() => new();

	public Continuum WaitFor(Func<bool> condition) {
		_continuum = new Continuum { _condition = condition, _type = Type.WaitFor };
		return _continuum;
	}

	public Continuum Do(Action action) {
		_continuum = new Continuum { _action = action, _type = Type.Do };
		return _continuum;
	}

	public void Update() {
		switch (_type) {
			case Type.Start:
				if (_continuum is null) {
					_type = Type.End;
					return;
				}
				_type = _continuum._type;
				_action = _continuum._action;
				_condition = _continuum._condition;
				_continuum = _continuum._continuum;
				Update();
				break;

			case Type.WaitFor:
				if (_condition.Invoke()) {
					if (_continuum is null) {
						_type = Type.End;
						return;
					}
					_type = _continuum._type;
					_action = _continuum._action;
					_condition = _continuum._condition;
					_continuum = _continuum._continuum;
					Update();
				}
				break;

			case Type.Do:
				_action.Invoke();
				if (_continuum is null) {
					_type = Type.Start;
					return;
				}
				_type = _continuum._type;
				_action = _continuum._action;
				_condition = _continuum._condition;
				_continuum = _continuum._continuum;
				break;

			case Type.End: return;
		}
	}
}