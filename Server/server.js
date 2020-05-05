var io = require("socket.io")(5000);

var monsterType;
var monsterMaxHealth;
var monsterCurrentHealth;
var monsterDamage;
var mosterWeakness = false;
SpawnMonster();

console.log("MonsterType : " + monsterType);
console.log("MonsterMaxHealth : " + monsterMaxHealth);
console.log("MonsterCurrent : " + monsterCurrentHealth);
console.log("MonsterDamage : " + monsterDamage);

io.on("connection", (socket) => {
    if (monsterCurrentHealth < monsterMaxHealth) {
        UpdateMonsterData();
    } else {
        socket.emit("SpawnMonster", MonsterData);
    }

    socket.on("PlayerAttack", (data) => {
        
        if (monsterCurrentHealth > 0) {
            if (mosterWeakness == true) {
                var PlayerDamage = JSON.parse(JSON.stringify(data));
                monsterCurrentHealth -= (PlayerDamage.Damage * 2);
                console.log("MonsterCurrentHealth : " + monsterCurrentHealth);
                UpdateMonsterData();

                setTimeout(function () {
                    io.emit("Buffend");
                    return mosterWeakness = true;
                }, 7000);
            } else {
                var PlayerDamage = JSON.parse(JSON.stringify(data));
                monsterCurrentHealth -= PlayerDamage.Damage;
                console.log("MonsterCurrentHealth : " + monsterCurrentHealth);
                UpdateMonsterData();
            }
        }else{
            io.emit("MonsterDie");
            setTimeout(function(){
                ReSpawnMonster();
            },5000);
        }


    })


    socket.on("PlayerBuff", function () {
        io.emit("BuffAttack");
        return mosterWeakness = true;
    })
    console.log("client connected");

    socket.on("PlayerHeal", function () {
        io.emit("Heal");
    })

})
console.log("server started");

setInterval(() => {
    var data = {
        MonsterDamage: monsterDamage
    }
    io.emit("MonsterAttack", data)
}, 10000);


function SpawnMonster() {
    monsterType = Math.floor(Math.random() * 3) + 1;
    monsterMaxHealth = (Math.floor(Math.random() * 3) + 1) * 50;
    monsterCurrentHealth = monsterMaxHealth;
    monsterDamage = (Math.floor(Math.random() * 5) + 1) * 2;
}

var MonsterData = {
    MonsterType: monsterType,
    MonsterMaxHealth: monsterMaxHealth,
    MonsterCurrentHealth: monsterCurrentHealth,
}

function ReSpawnMonster(){
    SpawnMonster();
    var MonsterData = {
        MonsterType: monsterType,
        MonsterMaxHealth: monsterMaxHealth,
        MonsterCurrentHealth: monsterCurrentHealth,
    }
    io.emit("SpawnMonster", MonsterData)
}

function UpdateMonsterData() {
    var NewMonsterData = {
        MonsterType: monsterType,
        MonsterMaxHealth: monsterMaxHealth,
        MonsterCurrentHealth: monsterCurrentHealth,
    }
    io.emit("UpdateMonsterData", NewMonsterData);
}

