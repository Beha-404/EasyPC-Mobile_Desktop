# EasyPC - Inteligentna Platforma za Sastavljanje Računara
**Seminarski rad iz Razvoj softvera II**

EasyPC je napredna multi-platformska aplikacija za sastavljanje i kupovinu računara sa inteligentnim sistemom za provjeru kompatibilnosti i korak-po-korak vodičem za konfiguraciju računara.

**Pristupni podaci:**

--Sifra zip file-a od BUILD-a je "fit"

- Desktop aplikacija: `desktop` / `test` ili 
`superadmin` / `superadmin123` ako zelimo imati dodatne funkcije

- Mobilna aplikacija: `mobile` / `test`

**Test kartica:** `4242 4242 4242 4242`
**Test PayPal:** @email:`rs2-flutter@personal.example.com` @password:`flutter123`

Mobilna aplikacija je pokrenuta preko emulatora: **Pixel_9a**, ili preko svog uređaja.

---

## EasyPC - Sastavi Računar Svojih Snova

EasyPC je multi-platformska aplikacija za sastavljanje i kupovinu računara sa desktop aplikacijom (Flutter), mobilnom aplikacijom (Flutter) i sistemom preporuke.

## 🚀 Funkcionalnosti

### 💻 Desktop Aplikacija (Flutter)
- Admin panel za upravljanje proizvodima
- **Compatibility Checker** interfejs
- **Build Wizard** interfejs
- Upravljanje korisnicima i narudžbama
- Real-time support chat (SignalR)
- PDF izvještaji

### 📱 Mobilna Aplikacija (Flutter)
- Pregled PC konfiguracija
- **Compatibility Checker** (optimizovan za mobilne uređaje)
- **Build Wizard** (vertikalni stepper)
- Korpa i naručivanje
- Korisnički profil

### 🔧 **Compatibility Checker**
- Automatska provjera kompatibilnosti komponenti
- Provjera socket-a (CPU ↔ Matična ploča)
- Validacija form faktora (Matična ploča ↔ Kućište)
- Provjera napajanja
- Detekcija bottleneck-a (CPU/GPU balans)
- Sistem bodovanja (0-100 bodova)
- Vizuelne preporuke i upozorenja

### 🧙 **Build Wizard**
- Vodič kroz 7 koraka za sastavljanje računara
  1. Tip računara (Gaming, Office, Workstation)
  2. Procesor (filtriran po tipu računara)
  3. Matična ploča (kompatibilni socket-i)
  4. RAM memorija (optimalne brzine)
  5. Grafička kartica (balansirana sa CPU-om)
  6. Napajanje (preporučena snaga)
  7. Kućište (kompatibilni form faktori)
- Provjera kompatibilnosti u realnom vremenu
- Dinamički izračun cijene
- Pametno filtriranje komponenti
- Funkcionalnost čuvanja i naručivanja


## 🛠️ Tehnologije

| Sloj | Tehnologija |
|------|-------------|
| **Backend** | .NET 9, ASP.NET Core Web API |
| **Baza podataka** | SQL Server 2022, Entity Framework Core |
| **Autentifikacija** | JWT Tokens, Basic Auth |
| **Real-time** | SignalR (Support Chat) |
| **Message Queue** | RabbitMQ |
| **Desktop & Mobile** | Flutter 3.x, Dart |
| **Kontejnerizacija** | Docker, Docker Compose |

## 📦 Instalacija i Pokretanje

### 1. Docker (Preporučeno)
```bash
cd EasyPC
docker-compose up -d --build
```
**Servisi:**
- API: `http://localhost:5285`
- SQL Server: `localhost:1433`
- RabbitMQ: `localhost:15672` (guest/guest)

### 2. Desktop Aplikacija
```bash
cd UI/easy_pc_admin
flutter pub get
flutter run -d windows
```

**Login:** `desktop` / `test`

### 3. Mobilna Aplikacija
```bash
cd UI/easy_pc_mobile
flutter pub get
flutter run
```

**Login:** `mobile` / `test`

---

## 🎮 Kako Koristiti

### Compatibility Checker
1. Kliknite na "Compatibility" u navigaciji
2. Odaberite komponente iz padajućih menija
3. Kliknite "Check Compatibility"
4. Pregledajte rezultate:
   - ✅ **Zeleno:** Sve kompatibilno
   - ⚠️ **Narandžasto:** Upozorenja
   - ❌ **Crveno:** Nekompatibilno

### Build Wizard
1. Kliknite na "Build Wizard" u navigaciji
2. Pratite 7 koraka
3. Odaberite komponentu sa liste
4. Pregledajte cijenu u realnom vremenu
5. Na kraju: "Save Build"

---

⭐ **Razlika od klasičnih e-commerce projekata:**
- ✅ Automatska provjera kompatibilnosti
- ✅ Inteligentni Build Wizard sa 7 koraka
- ✅ Filtriranje u realnom vremenu na osnovu prethodnih izbora
- ✅ Detekcija bottleneck-a
- ✅ Sistem bodovanja kompatibilnosti
- ✅ Preporuke za napajanje i balans komponenti
- ✅ Real-time filtering based on previous choices
- ✅ Bottleneck detection
- ✅ Compatibility scoring system
- ✅ Power supply and component balance recommendations
