# HealSCPs

HealSCPs is a simple plugin which lets you heal any SCP with a healing item. (Except 079)

![Version](https://img.shields.io/github/v/release/Skillz2play/HealSCPs?label=Version)
![Downloads](https://img.shields.io/github/downloads/Skillz2play/HealSCPs/total?style=flat-square)


## Installation

Go to the latest [Release](https://github.com/Skillz2play/HealSCPs/releases) and download the .dll file.

Next, go to your Plugins folder and drop the .dll where you have installed all your other EXILED Plugins.

## How to use

Use the command `.heal` near an scp with a medical item and it will heal them with the amount set in the config.

## Default Config

```yaml
heal_s_c_ps:
  is_enabled: true
  # How far do the SCPs have to be for the health to be applied?
  distance: 5
  # What SCPs are allowed to be healed?
  allowed_scps:
  - Scp049
  - Scp0492
  - Scp079
  - Scp096
  - Scp106
  - Scp173
  - Scp939
  - Scp3114
  # Can non-SCPs get healed?
  heal_non_s_c_ps: false
  allowed_heals:
    Medkit:
      instant_heal_amount: 65
      effect_info: []
      apply_original_effects: false
    SCP500:
      instant_heal_amount: 0
      effect_info: []
      apply_original_effects: true
    SCP207:
      instant_heal_amount: 50
      effect_info: []
      apply_original_effects: false
    Adrenaline:
      instant_heal_amount: 50
      effect_info:
      - effect_type: Invigorated
        should_add_if_present: true
        time: 5
        effect_amount: 1
      apply_original_effects: false
    Painkillers:
      instant_heal_amount: 50
      effect_info: []
      apply_original_effects: true
```

![Alt](https://repobeats.axiom.co/api/embed/ac7c5b5af442a893158dd41f6d6e6b05f1f09b7d.svg "Repobeats analytics image")
