# HealSCPs

HealSCPs is a simple plugin which lets you heal any SCP with a healing item. (Except 079)

## Installation

Go to the latest [Release](https://github.com/Skillz2play/HealSCPs/releases) and download the .dll file.

Next go to your Plugins folder and drop the dll where you have installed all your other EXILED Plugins.

## How to use

Use the command .heal near an scp with a medical item and it will heal them with the amount set in the config.

## Config

```cs
heal_s_c_ps:
  is_enabled: true
  # How much health the SCP gets from a Medkit.
  medkit_health_recieve: 75
  # How much health the SCP gets from SCP 500.
  s_c_p500_health_recieve: 100
  # How much health the SCP gets from Adrenaline.
  adrenaline_health_recieve: 35
  # How much health the SCP gets from Painkillers.
  painkillers_health_recieve: 10
  # How much health the SCP gets from SCP 207.
  s_c_p207_health_recieve: 25
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
  - Scp93953
```
