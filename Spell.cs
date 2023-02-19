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
using Continuum = Extensions.Continuum;
using Methods = Extensions.Methods;
using Object = System.Object;
using Random = UnityEngine.Random;

namespace Strength;
public class Spell : SpellCastCharge {
	public float strengthMultiplier = 2.0f;
	public override void Load(SpellCaster spellCaster, Level level) {
		base.Load(spellCaster, level);
		Player.currentCreature.handLeft.bodyDamager.data.addForce *= strengthMultiplier;
		Player.currentCreature.handLeft.bodyDamager.data.addForceRagdollPartMultiplier *= strengthMultiplier;
		Player.currentCreature.handLeft.bodyDamager.data.addForceRagdollOtherMultiplier *= strengthMultiplier;
		Player.currentCreature.handLeft.bodyDamager.data.addForceSlowMoMultiplier *= strengthMultiplier;
		Player.currentCreature.handRight.bodyDamager.data.addForce *= strengthMultiplier;
		Player.currentCreature.handRight.bodyDamager.data.addForceRagdollPartMultiplier *= strengthMultiplier;
		Player.currentCreature.handRight.bodyDamager.data.addForceRagdollOtherMultiplier *= strengthMultiplier;
		Player.currentCreature.handRight.bodyDamager.data.addForceSlowMoMultiplier *= strengthMultiplier;
	}
	public override void Unload() {
		base.Unload();
		Player.currentCreature.handLeft.bodyDamager.data.addForce /= strengthMultiplier;
		Player.currentCreature.handLeft.bodyDamager.data.addForceRagdollPartMultiplier /= strengthMultiplier;
		Player.currentCreature.handLeft.bodyDamager.data.addForceRagdollOtherMultiplier /= strengthMultiplier;
		Player.currentCreature.handLeft.bodyDamager.data.addForceSlowMoMultiplier /= strengthMultiplier;
		Player.currentCreature.handRight.bodyDamager.data.addForce /= strengthMultiplier;
		Player.currentCreature.handRight.bodyDamager.data.addForceRagdollPartMultiplier /= strengthMultiplier;
		Player.currentCreature.handRight.bodyDamager.data.addForceRagdollOtherMultiplier /= strengthMultiplier;
		Player.currentCreature.handRight.bodyDamager.data.addForceSlowMoMultiplier /= strengthMultiplier;
	}
}