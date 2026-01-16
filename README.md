# Flappy Bird Clone (Unity) 🐦
![Unity](https://img.shields.io/badge/unity-%23000000.svg?style=for-the-badge&logo=unity&logoColor=white)
![C#](https://img.shields.io/badge/c%23-%23239120.svg?style=for-the-badge&logo=c-sharp&logoColor=white)
![.Net](https://img.shields.io/badge/.NET-5C2D91?style=for-the-badge&logo=.net&logoColor=white)
Unity oyun motoru ve C# kullanılarak geliştirilmiş, popüler Flappy Bird oyununun klonudur.

## 🎮 Özellikler
* **Sonsuz Oynanış:** Dinamik olarak üretilen boru sistemi (Object Pooling mantığı ile).
* **Skin Sistemi:** Menüden farklı kuş görünümleri seçebilme özelliği.
* **Kayıt Sistemi:** `PlayerPrefs` kullanılarak En Yüksek Skorun (High Score) hafızada tutulması.
* **Parallax Efekt:** Hareketli zemin ve arka plan ile derinlik algısı.

## 🏗️ Teknik Mimari (Technical Details)
Proje, Nesne Yönelimli Programlama (OOP) prensiplerine uygun olarak geliştirilmiştir:
* **GameManager:** Singleton tasarım deseni ile oyunun durumu (State) ve UI yönetimi.
* **Spawner:** Rastgele (Random) algoritmalarla boru üretimi.
* **Physics:** Unity'nin Rigidbody2D fiziği ile çarpışma ve yerçekimi kontrolü.

## 🕹️ Nasıl Oynanır?
* Ekrana tıklayarak (veya Mouse Sol Tık) kuşu zıplatın.
* Borulara çarpmadan ilerleyin.

## 🛠️ Kullanılan Teknolojiler
* Unity 2021 
* C#
 
## 👨‍💻 İletişim
**Eray Meriç** - Bilgisayar Mühendisliği Öğrencisi
* **LinkedIn:** [https://www.linkedin.com/in/eray-meri%C3%A7-950089340/]
* **Email:** [eraymeric51@hotmail.com]

## 🚀 Kurulum (Installation)
Bu projeyi yerel makinenizde çalıştırmak için aşağıdaki adımları izleyin:
1.  Projeyi klonlayın:
    ```bash
    git clone [https://github.com/ErayMericDev/FlappyBirdClone.git]
    ```
2.  Unity Hub'ı açın ve **"Open"** butonuna tıklayarak klasörü seçin.
3.  **Assets > Scenes** klasörüne gidin ve `GameScene` sahnesini açın.
4.  Editördeki **Play** tuşuna basarak oyunu başlatın.
*Not: Proje Unity 2021.3 sürümü ile geliştirilmiştir. Daha eski sürümlerde uyumluluk sorunu yaşanabilir.*  

<img width="925" height="506" alt="Ekran görüntüsü 2026-01-16 213702" src="https://github.com/user-attachments/assets/03541731-465a-46bb-9206-19abf590be18" />
<img width="1084" height="504" alt="Ekran görüntüsü 2026-01-16 213741" src="https://github.com/user-attachments/assets/6b613fe8-76b0-4020-9c67-c5d16607a302" />
<img width="782" height="495" alt="Ekran görüntüsü 2026-01-16 213750" src="https://github.com/user-attachments/assets/ef114ff5-eb3b-4b4b-b2a5-0485a44feeef" />
