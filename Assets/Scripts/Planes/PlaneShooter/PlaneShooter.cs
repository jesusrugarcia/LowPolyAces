using System;
using UnityEngine;

public enum shootingType{
    normal,
    onButton,
    turret
}

public class PlaneShooter : MonoBehaviour
{
    public GameObject bullet;
    public GameObject missile;
    public GameObject gadget;
    public GameObject defense;
    public float shootTimer = 0;
    public PlaneManager plane;
    public shootingType type;

    public bool magazineFull = false;
    public int magazine;
    public float shootSpeedOnButton = 0.05f;
    public float defenseRecharge = 0;
    public float specialRecharge = 0;

    public bool defenseActivated = false;

    public GameObject[] mines;
    public GameObject hook;
    public GameObject firstCoin;
    public GameObject lastCoin;
    
    private void Start() {
        
    }
   
    private void FixedUpdate() {
        checkShoot();
        rechargeSpecial();
        rechargeDefense();
    }

    public void rechargeSpecial(){
        if (plane.stats.specialAmmo < plane.stats.maxSpecialAmmo){
            specialRecharge += Time.deltaTime;
            if(specialRecharge >= plane.stats.rechargeSpecialTime){
                specialRecharge = 0;
                plane.stats.specialAmmo ++;
            }
        } else {
            specialRecharge = 0;
        }
        
    }

    public void rechargeDefense(){
        if(plane.stats.defenseAmmo < plane.stats.maxDefenseAmmo){
            defenseRecharge += Time.deltaTime;
            if(defenseRecharge >= plane.stats.rechargeDefenseTime){
                defenseRecharge = 0;
                plane.stats.defenseAmmo ++;
            }
        } else {
            defenseRecharge = 0;
        }
        
    }

    public virtual void checkShoot(){
        if(type == shootingType.normal){
            shootTimer += Time.deltaTime;
            if (shootTimer >= plane.stats.shootSpeed){
                shootTimer = 0;
                shoot();
            }
        } else if(type == shootingType.onButton){
            if(magazineFull){
                onButtonShoot();
            } else {
                shootTimer += Time.deltaTime;
                if (shootTimer >= plane.stats.shootSpeed){
                    magazineFull = true;
                    magazine = plane.stats.magazineSize;
                    shootTimer = 0;
                }
            }
        }
    }

    public virtual void onButtonShoot(){
        shootTimer += Time.deltaTime;
        if(shootTimer > shootSpeedOnButton){
            shootTimer = 0;
            shoot();
            magazine--;
            if(magazine <= 0){
                magazineFull = false;
            }
        }
    }

    public virtual void shoot(){
        if (plane.stats.statusEffects[(int)StatusEffects.Rusting] > 0){
            if (UnityEngine.Random.Range(0,2) == 0){
                //sound effect not shooting
                magazine = 0;
                return;
            }
        }
        instantiateBullet();
        if (plane.stats.extraBullets > 0){
            var offset = 45 / plane.stats.extraBullets;
            for (int i = 0; i < plane.stats.extraBullets; i++){
                instantiateBullet(angle: offset * (i+1));
                instantiateBullet(angle: -offset * (i+1));
            }
        }
    }

    public virtual void instantiateBullet(float angle = 0){
        var bull = Instantiate(bullet, transform.position, transform.rotation);
        var bullMovement = bull.GetComponent<BulletMovement>();
        if (bullMovement.isMelee){
                instantiateMeleeBullet(bull, angle);
        } else if (plane.stats.trackerBullet){
            instantiateMissileBullet(bull,angle);
        }else {
            instantiateNormalBullet(bull, angle);
        }
        
    }

    public void instantiateMissileBullet(GameObject bull, float angle){
        Destroy(bull.GetComponent<BulletMovement>());
            MissileManager manager;
            try{
                manager = bull.GetComponent<MissileManager>();
                if(manager == null){
                    manager = bull.AddComponent<MissileManager>();
                }
            } catch (Exception e){
                Debug.Log(e);
                manager = bull.AddComponent<MissileManager>();
            }
            
            manager.plane = plane;
            manager.teamManager = GetComponent<TeamManager>();
            manager.teamManager.team = plane.teamManager.team;
            bull.GetComponent<DamageManager>().damage = plane.stats.bulletDamage;
            bull.transform.Rotate(new Vector3(0,0,angle));
    }

    public void instantiateMeleeBullet(GameObject bull, float angle){
            bull.GetComponent<TeamManager>().team = plane.teamManager.team;
            var bullMovement = bull.GetComponent<BulletMovement>();
            bullMovement.controller = plane.controller;
            bullMovement.plane = plane;
            bull.GetComponent<DamageManager>().damage = plane.stats.drillDamage;
            bullMovement.meleeOffset = angle * 0.01f;
    }

    public void instantiateNormalBullet(GameObject bull, float angle){
            bull.GetComponent<TeamManager>().team = plane.teamManager.team;
            var bullMovement = bull.GetComponent<BulletMovement>();
            bullMovement.controller = plane.controller;
            bullMovement.plane = plane;
            bull.GetComponent<DamageManager>().damage = plane.stats.bulletDamage;
            bull.transform.Rotate(new Vector3(0,0,angle));
            try{
                var manager = bull.GetComponent<MissileManager>();
                if(manager != null){
                    manager.plane = plane;
                }
            } catch (Exception e){
                Debug.Log(e);
            }
            
    }

    public virtual void shootTurret(GameObject shooter){
        var bull = Instantiate(bullet, shooter.transform.position, shooter.transform.rotation);
        bull.GetComponent<TeamManager>().team = plane.teamManager.team;
        bull.GetComponent<DamageManager>().damage = plane.stats.turretDamage;
        bull.GetComponent<BulletMovement>().controller = plane.controller;
    }

    public void launchMissile(){
        if(plane.stats.missileType == MissileType.Missile || plane.stats.missileType == MissileType.ClusterMissile){
            var mis = Instantiate(missile, transform.position + transform.right * 1, transform.rotation);
            mis.GetComponent<MissileManager>().plane = plane;
            mis.GetComponent<DamageManager>().damage = plane.stats.missileDamage;
            plane.stats.specialAmmo --;
        } else if(plane.stats.missileType == MissileType.Mine && plane.stats.mines < plane.stats.maxMines){
            if(mines.Length <= 0){
                createMinesList();
            }
            spawnMine();
        } else if (plane.stats.missileType == MissileType.Flare){
            var mis = Instantiate(missile, transform.position + transform.right * 1, transform.rotation);
            mis.GetComponent<MissileManager>().plane = plane;
            mis.GetComponent<MissileManager>().isFlare = true;
            plane.stats.specialAmmo --;
        } else if (plane.stats.missileType == MissileType.SupportClaw){
            var claw = Instantiate(missile, transform.position + transform.right * 1, transform.rotation);
            claw.GetComponent<MissileManager>().plane = plane;
            claw.GetComponent<SupportClawManager>().plane = plane;
            plane.stats.specialAmmo --;
        } else if(plane.stats.missileType == MissileType.ElectricClaw || plane.stats.missileType == MissileType.BurningClaw || plane.stats.missileType == MissileType.RustingClaw){
            var claw = Instantiate(missile, transform.position + transform.right * 1, transform.rotation);
            claw.GetComponent<MissileManager>().plane = plane;
            claw.GetComponent<ClawManager>().plane = plane;
            plane.stats.specialAmmo --;
        }
        
        
    }

    public virtual void launchGadget(){
        if (plane.stats.gadgetType == GadgetType.Turbo){
            plane.stats.speed = plane.stats.maxSpeed * 4;
            var turbo = Instantiate(gadget, transform.position, transform.rotation);
            plane.stats.specialAmmo --;
        } else if (plane.stats.gadgetType == GadgetType.TankShield){
            var tankShield = Instantiate(gadget, transform.position, transform.rotation);
            tankShield.GetComponent<TankShieldManager>().plane = plane;
            plane.stats.specialAmmo --;
        } else if (plane.stats.gadgetType == GadgetType.AreaOfHeal){
            var area = Instantiate(gadget, transform.position, transform.rotation);
            area.GetComponent<HealAreaManager>().plane = plane;
            plane.stats.specialAmmo --;
        } else if (plane.stats.gadgetType == GadgetType.Laser && !plane.stats.laserActivated){
            try{
                if (GetComponent<LaserManager>() == null){
                    gameObject.AddComponent<LaserManager>().plane = plane;
                }
            } catch (Exception e){
                Debug.Log(e);
            }
            plane.stats.laserActivated = true;
            plane.stats.specialAmmo --;
        } else if(plane.stats.gadgetType == GadgetType.Coin){
            manageCoin();
        } if (plane.stats.gadgetType == GadgetType.FireTurbo){
            plane.stats.speed = plane.stats.maxSpeed * 6;
            var turbo = Instantiate(gadget, transform.position, transform.rotation);
            plane.stats.specialAmmo --;

            var auxBullet = bullet;
            bullet = plane.controller.centralManager.FireDrill;
            shoot();
            bullet = auxBullet;
        }

    }

    public virtual void Defense(){
        if (plane.stats.defenseType == DefenseType.Dash){
            plane.stats.defenseAmmo --;
            plane.statusManager.addStatus(StatusEffects.Invulnerability , plane.stats.invIncrease);
            transform.Translate(Vector3.right * 2);
            var dash = Instantiate(defense, transform.position, transform.rotation);
            var particles = Instantiate(plane.controller.centralManager.InvulnerabilityParticleEffect, transform.position, transform.rotation);
            particles.GetComponent<ParticleEffectManager>().plane = plane;
        } else if (plane.stats.defenseType == DefenseType.HealShield && !defenseActivated){
            defenseActivated = true;
            var healShield = Instantiate(defense, transform.position, transform.rotation);
            healShield.GetComponent<HealShieldManager>().plane = plane;
        } else if (plane.stats.defenseType == DefenseType.Ghost){
            var particles = Instantiate(plane.controller.centralManager.GhostParticleEffect, transform.position, transform.rotation);
            particles.GetComponent<ParticleEffectManager>().plane = plane;
            plane.stats.defenseAmmo --;
            plane.statusManager.addStatus(StatusEffects.Ghost, plane.stats.ghostIncrease);
        } else if (plane.stats.defenseType == DefenseType.Hook || plane.stats.defenseType == DefenseType.InverseHook){
            manageHook();
        }

    }

    public void manageHook(){
        if (hook == null){
            hook = Instantiate(defense, transform.position + transform.right * 1, transform.rotation);
            hook.GetComponent<HookManager>().plane = plane;
            
        } else if(hook.activeSelf == false) {
            hook.transform.position = transform.position + transform.right * 1;
            hook.transform.rotation = transform.rotation;
            hook.GetComponent<HookManager>().hook.transform.position = transform.position + transform.right * 1;
            hook.GetComponent<HookManager>().hook.transform.rotation = transform.rotation;
            hook.SetActive(true);
            
        } else {
            plane.statusManager.addStatus(StatusEffects.Invulnerability,10);
            plane.stats.defenseAmmo --;
            hook.GetComponent<HookManager>().move = true;
        }
    }

    public void manageCoin(){
        var coin = Instantiate(gadget, transform.position, Quaternion.identity);
        var coinManager = coin.GetComponent<CoinManager>();
        coinManager.teamManager.team = plane.teamManager.team;

        if(lastCoin != null){
            var oldCoin = lastCoin.GetComponent<CoinManager>();
            oldCoin.next = coin;
            oldCoin.rotateTowards();
            coinManager.previous = lastCoin;
        }
        
        lastCoin = coin;
        if (firstCoin == null){
            firstCoin = coin;
        }

        coinManager.plane = plane;
        plane.stats.specialAmmo --;
        coinManager.rotateTowards();
    }

    public void createMinesList(){
        mines = new GameObject[plane.stats.maxMines];
    }

    public void spawnMine(){
        for (int i = 0; i< plane.stats.maxMines; i++){
            if (mines[i] == null || mines[i].activeSelf == false){
            var mis = Instantiate(missile, transform.position + transform.right * 1, transform.rotation);
            mis.GetComponent<TeamManager>().team = plane.teamManager.team;
            mis.GetComponent<DamageManager>().damage = plane.stats.mineDamage;
            var mina = mis.GetComponent<Mine>();
            mina.plane = gameObject;
            mina.pos = i;
            plane.stats.mines ++;
            plane.stats.specialAmmo --;
            mines[i] = mis;
            break;
            } 
        }
    }

    public void resizeMinesList(){
        var newMines = mines = new GameObject[plane.stats.maxMines];
        for(int i= 0; i< mines.Length; i++){
            newMines[i] = mines [i];
            mines = newMines;
        }
    }


}
