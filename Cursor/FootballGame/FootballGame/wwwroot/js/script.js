const canvas = document.getElementById('wheelCanvas');
const ctx = canvas.getContext('2d');
const spinButton = document.getElementById('spinButton');
const settingsButton = document.getElementById('settingsButton');
const leagueModal = new bootstrap.Modal(document.getElementById('leagueModal'));
const leagueSelect = document.getElementById('leagueSelect');
const confirmLeague = document.getElementById('confirmLeague');

// API-Football yapılandırması
const API_KEY = 'ca7e92a62d0342e14cbc39b6f55b2821'; // RapidAPI anahtarınızı buraya ekleyin
const API_URL = 'https://v3.football.api-sports.io';

// Lig ID'leri
const leagueIds = {
    'premier-league': 39,  // Premier League
    'la-liga': 140,       // La Liga
    'bundesliga': 78,     // Bundesliga
    'serie-a': 135,       // Serie A
    'ligue-1': 61,        // Ligue 1
    'super-lig': 203,     // Süper Lig
    'eredivisie': 88,     // Eredivisie
    'primeira-liga': 94,  // Primeira Liga
    'belgian-pro-league': 144, // Belgian Pro League
    'brasileirao': 71     // Brasileirão
};

// Renk paleti
const teamColors = [
    '#FF6B6B', '#4ECDC4', '#45B7D1', '#96CEB4', '#FFEEAD',
    '#D4A5A5', '#7FDBFF', '#01FF70', '#FFDC00', '#FF4136',
    '#B10DC9', '#39CCCC', '#01FF70', '#FFDC00', '#FF851B',
    '#85144b', '#F012BE', '#3D9970', '#111111', '#AAAAAA'
];

// Renk parlaklığını hesaplama fonksiyonu
function getContrastColor(hexcolor) {
    const r = parseInt(hexcolor.substr(1,2), 16);
    const g = parseInt(hexcolor.substr(3,2), 16);
    const b = parseInt(hexcolor.substr(5,2), 16);
    const yiq = ((r * 299) + (g * 587) + (b * 114)) / 1000;
    return (yiq >= 128) ? '#000000' : '#FFFFFF';
}

// API'den takımları çekme fonksiyonu
async function fetchTeams(leagueId) {
    try {
        const response = await fetch(`${API_URL}/teams?league=${leagueId}&season=${new Date().getFullYear()}`, {
            method: 'GET',
            headers: {
                'x-rapidapi-host': 'api-football-v1.p.rapidapi.com',
                'x-rapidapi-key': API_KEY
            }
        });

        if (!response.ok) {
            throw new Error('API isteği başarısız oldu');
        }

        const data = await response.json();
        return data.response.map((team, index) => ({
            text: team.team.name,
            color: teamColors[index % teamColors.length]
        }));
    } catch (error) {
        console.error('Takımlar çekilirken hata oluştu:', error);
        alert('Takımlar yüklenirken bir hata oluştu. Lütfen daha sonra tekrar deneyin.');
        return null;
    }
}

let currentRotation = 0;
let isSpinning = false;
let currentSegments = [];

function drawWheel() {
    const centerX = canvas.width / 2;
    const centerY = canvas.height / 2;
    const radius = Math.min(centerX, centerY) - 10;

    ctx.clearRect(0, 0, canvas.width, canvas.height);

    if (currentSegments.length === 0) {
        // Eğer takım seçilmemişse varsayılan mesajı göster
        ctx.fillStyle = '#000';
        ctx.font = 'bold 20px Arial';
        ctx.textAlign = 'center';
        ctx.fillText('Lütfen bir lig seçin', centerX, centerY);
        return;
    }

    const segmentAngle = (2 * Math.PI) / currentSegments.length;

    currentSegments.forEach((segment, index) => {
        const startAngle = index * segmentAngle + currentRotation;
        const endAngle = (index + 1) * segmentAngle + currentRotation;

        // Çizim segmenti
        ctx.beginPath();
        ctx.moveTo(centerX, centerY);
        ctx.arc(centerX, centerY, radius, startAngle, endAngle);
        ctx.closePath();
        ctx.fillStyle = segment.color;
        ctx.fill();
        ctx.strokeStyle = '#333';
        ctx.lineWidth = 1;
        ctx.stroke();

        // Metin ekleme
        ctx.save();
        ctx.translate(centerX, centerY);
        ctx.rotate(startAngle + segmentAngle / 2);
        ctx.textAlign = 'right';
        // Arka plan rengine göre metin rengini ayarla
        ctx.fillStyle = getContrastColor(segment.color);
        ctx.font = 'bold 14px Arial';
        // Metni beyaz gölge ile çevreleme
        if (ctx.fillStyle === '#000000') {
            ctx.shadowColor = 'white';
            ctx.shadowBlur = 4;
        } else {
            ctx.shadowColor = 'black';
            ctx.shadowBlur = 4;
        }
        ctx.fillText(segment.text, radius - 20, 5);
        ctx.restore();
    });

    // Merkez nokta
    ctx.beginPath();
    ctx.arc(centerX, centerY, 10, 0, 2 * Math.PI);
    ctx.fillStyle = '#333';
    ctx.fill();
    ctx.strokeStyle = '#000';
    ctx.lineWidth = 2;
    ctx.stroke();
}

function spinWheel() {
    if (isSpinning || currentSegments.length === 0) return;
    
    isSpinning = true;
    spinButton.disabled = true;

    const spinDuration = 3000; // 3 saniye
    const startTime = Date.now();
    const startRotation = currentRotation;
    const totalSpins = 5 + Math.random() * 5; // 5-10 tam tur
    const finalRotation = startRotation + (totalSpins * 2 * Math.PI);

    function animate() {
        const elapsed = Date.now() - startTime;
        const progress = Math.min(elapsed / spinDuration, 1);

        // Easing function
        const easing = 1 - Math.pow(1 - progress, 3);
        currentRotation = startRotation + (finalRotation - startRotation) * easing;

        drawWheel();

        if (progress < 1) {
            requestAnimationFrame(animate);
        } else {
            isSpinning = false;
            spinButton.disabled = false;
            
            // Kazanan takımı hesapla
            const segmentAngle = (2 * Math.PI) / currentSegments.length;
            const normalizedRotation = currentRotation % (2 * Math.PI);
            const winningIndex = Math.floor((2 * Math.PI - normalizedRotation) / segmentAngle) % currentSegments.length;
            alert(`Kazanan Takım: ${currentSegments[winningIndex].text}!`);
        }
    }

    animate();
}

// Event Listeners
document.addEventListener('DOMContentLoaded', function() {
    // Seçenekler butonu tıklama olayı
    settingsButton.addEventListener('click', function() {
        leagueModal.show();
    });

    // Lig seçimi onaylama
    confirmLeague.addEventListener('click', async function() {
        const selectedLeague = leagueSelect.value;
        if (selectedLeague && leagueIds[selectedLeague]) {
            // Yükleniyor durumunu göster
            confirmLeague.disabled = true;
            confirmLeague.innerHTML = '<span class="spinner-border spinner-border-sm" role="status" aria-hidden="true"></span> Yükleniyor...';

            // Takımları API'den çek
            const teams = await fetchTeams(leagueIds[selectedLeague]);
            
            if (teams) {
                currentSegments = teams;
                currentRotation = 0;
                drawWheel();
                leagueModal.hide();
            }

            // Butonu normale döndür
            confirmLeague.disabled = false;
            confirmLeague.textContent = 'Tamam';
        } else {
            alert('Lütfen bir lig seçin!');
        }
    });

    // Çarkı çevirme butonu
    spinButton.addEventListener('click', spinWheel);

    // İlk çizim
    drawWheel();
}); 