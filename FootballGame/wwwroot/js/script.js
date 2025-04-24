const canvas = document.getElementById("wheelCanvas");
const ctx = canvas.getContext("2d");
let radius = canvas.width / 2;

let prizes = [];
let colors = [];
let anglePerSlice = 0;
let currentRotation = 0;

const API_KEY = "5c4b605fee49455ca1a928c422c51cb6"; // 🔐 Kendi API anahtarını buraya gir

// Ligleri yükle
async function loadLeagues() {
    const select = document.getElementById("leagueSelect");

    const response = await fetch("https://api.football-data.org/v4/competitions?plan=TIER_ONE", {
        headers: {
            "X-Auth-Token": API_KEY
        }
    });

    if (!response.ok) {
        alert("Lig listesi alınamadı.");
        return;
    }

    const data = await response.json();
    const competitions = data.competitions;

    select.innerHTML = ""; // Temizle
    competitions.forEach(competition => {
        const option = document.createElement("option");
        option.value = competition.code;
        option.textContent = `${competition.name} (${competition.area.name})`;
        select.appendChild(option);
    });
}

// Takımları çek ve çarkı çiz
async function loadTeams() {
    const leagueCode = document.getElementById("leagueSelect").value;
//http://api.football-data.org/v1/competitions/355/leagueTable
    const response = await fetch(`http://api.football-data.org/v1/competitions/${leagueCode}/teams`, {
        headers: {
            "X-Auth-Token": API_KEY
        }
    });

    if (!response.ok) {
        alert("Takım listesi alınamadı.");
        return;
    }

    const data = await response.json();
    prizes = data.teams.map(team => team.name);
    colors = prizes.map(() => getRandomColor());

    anglePerSlice = 2 * Math.PI / prizes.length;
    drawWheel();
}

// Rastgele renk üret
function getRandomColor() {
    const letters = "0123456789ABCDEF";
    let color = "#";
    for (let i = 0; i < 6; i++) {
        color += letters[Math.floor(Math.random() * 16)];
    }
    return color;
}

// Çarkı çiz
function drawWheel() {
    ctx.clearRect(0, 0, canvas.width, canvas.height);

    prizes.forEach((prize, i) => {
        const startAngle = i * anglePerSlice;
        const endAngle = startAngle + anglePerSlice;

        ctx.beginPath();
        ctx.moveTo(radius, radius);
        ctx.arc(radius, radius, radius, startAngle, endAngle);
        ctx.fillStyle = colors[i % colors.length];
        ctx.fill();
        ctx.stroke();

        ctx.save();
        ctx.translate(radius, radius);
        ctx.rotate(startAngle + anglePerSlice / 2);
        ctx.textAlign = "right";
        ctx.fillStyle = "#000";
        ctx.font = "bold 12px Arial";
        ctx.fillText(prize, radius - 10, 10);
        ctx.restore();
    });

    ctx.beginPath();
    ctx.arc(radius, radius, 40, 0, 2 * Math.PI);
    ctx.fillStyle = "#ffffff";
    ctx.fill();
    ctx.stroke();
}

// Çarkı döndür
function spinWheel() {
    if (prizes.length === 0) {
        alert("Lütfen önce bir lig seçip yükleyin.");
        return;
    }

    const spins = Math.floor(Math.random() * 3) + 5;
    const extraDeg = Math.floor(Math.random() * 360);
    const totalDeg = spins * 360 + extraDeg;

    currentRotation += totalDeg;

    canvas.style.transition = "transform 5s ease-out";
    canvas.style.transform = `rotate(${currentRotation}deg)`;

    setTimeout(() => {
        const normalizedDeg = currentRotation % 360;
        const normalizedRad = (360 - normalizedDeg) * Math.PI / 180;
        const index = Math.floor((normalizedRad % (2 * Math.PI)) / anglePerSlice);
        const prize = prizes[index];
        alert(`Kazandınız: ${prize}`);
    }, 5100);
}

// Sayfa yüklenince ligleri getir
window.onload = () => {
    loadLeagues();};


    window.loadTeams = loadTeams;