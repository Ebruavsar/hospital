# HASTA RANDEVU VE KAYIT SİSTEMİ

## 2023-2024 AKADEMİK YILI BAHAR DÖNEMİ
## BM442-Görsel Programlama

---

## İçindekiler
1. [Projenin Oluşturulması](#projenin-oluşturulması)
2. [Entity Layer](#entity-layer)
3. [Data Access Layer](#data-access-layer)
4. [Business Layer](#business-layer)
    - [Giriş](#giriş)
    - [Doktor İşlemleri](#doktor-i̇şlemleri)
    - [Hasta İşlemleri](#hasta-i̇şlemleri)
    - [Sekreter İşlemleri](#sekreter-i̇şlemleri)
    - [Randevu İşlemleri](#randevu-i̇şlemleri)
5. [Presentation Layer](#presentation-layer)
    - [Giriş](#giriş-1)
    - [Sekreter Giriş](#sekreter-giriş)
    - [Sekreter](#sekreter)
        - [Doktor İşlemleri](#doktor-i̇şlemleri)
        - [Sekreter İşlemleri](#sekreter-i̇şlemleri)
        - [Hasta İşlemleri](#hasta-i̇şlemleri)
        - [Randevu İşlemleri](#randevu-i̇şlemleri)
        - [Bölüme Göre Hasta Sayısı](#bölüme-göre-hasta-sayısı)
        - [Doktora Göre Hasta Sayısı](#doktora-göre-hasta-sayısı)
    - [Doktor Giriş](#doktor-giriş)
    - [Doktor](#doktor)
        - [PDF](#pdf)
        - [E-Posta](#e-posta)
        - [Randevu Geçmişi](#randevu-geçmişi)
6. [Setup](#setup)

---

## Projenin Oluşturulması
Projeyi yazabilmek için Microsoft Access kurulumunu yaptıktan sonra Visual Studio 2022 kurulumunu tamamladık. Access veritabanını oluşturmak için aşağıdaki tabloları oluşturduk. Sonrasında uygulamamızda kullanılacak olan “N Katmanlı Mimari” için projenin oluşturulması.

---

## Entity Layer
Bu katmanda oluşturduğumuz tabloları Access üzerinde erişebilmemiz için kendi uygulamamızda bu verileri tanımlıyoruz. Bu sınıf tüm veri tabanı tabloları için oluşturulmuştur. Entity layer uygulama mimarisi içinde genellikle katmanlı mimaride bulunan bir bileşenidir ve uygulamanın diğer katmanlarından bağımsızdır. Bu kodun daha modüler, bakımı kolay ve yeniden kullanılabilir olmasına katkıda bulunur.

---

## Data Access Layer
Bu sınıfın ana amacı veri tabanı bağlantısını yönetmektir. Bağlantı oluşturma, bağlantıyı açma, kapatma gibi temel işlemleri gerçekleştiren yöntemleri içerir. Connection sınıfı veri tabanı işlemlerini basitleştirmek ve veri tabanı bağlantısını yönetmek için kullanılır. Bu sınıf DataAccessLayer katmanında kullanılarak uygulamanın veri tabanı ile etkileşimini sağlar ve veri tabanı işlemlerini kolaylaştırır.

---

## Business Layer

### Giriş
Giris sınıfı, sekreter ve doktor kullanıcılarının kimlik ve şifre ile sisteme giriş yapmalarını sağlayan iki metod içerir: SekreterGiris ve DoktorGiris.

### Doktor İşlemleri
DoktorIslemleri sınıfı, bir doktorun bilgilerini veritabanına eklemek, silmek ve güncellemek için gerekli işlemleri gerçekleştiren bir iş mantığı (business) sınıfıdır.

### Hasta İşlemleri
HastaIslemleri sınıfı, bir hastanın bilgilerini veritabanına eklemek, silmek ve güncellemek için gerekli işlemleri gerçekleştiren bir iş mantığı (business) sınıfıdır.

### Sekreter İşlemleri
SekreterIslemleri sınıfı, bir sekreterin bilgilerini veritabanına eklemek, silmek ve güncellemek için gerekli işlemleri gerçekleştiren bir iş mantığı (business) sınıfıdır.

### Randevu İşlemleri
RandevuIslemleri sınıfı, hasta görüntüleme, poliklinik doktorlarını listeleme, randevu planlama ve doktor randevularını görüntüleme işlevlerini içerir.

---

## Presentation Layer

### Giriş
Giris sınıfı, kullanıcıların sisteme giriş yapmalarını sağlar.
![giriş](https://github.com/Ebruavsar/hospital/assets/73585933/2258c458-7f90-4576-b1c1-1161db176ade)

### Sekreter Giriş
SekreterGiris sınıfı, sekreter kullanıcılarının sisteme giriş yapmasını sağlar.
![image](https://github.com/Ebruavsar/hospital/assets/73585933/ef8b1c0d-9e8e-4fba-8038-dd52ad455f48)


### Sekreter
Sekreter sınıfı, sekreterlerin sistemde yapabileceği işlemleri içerir.

#### Doktor İşlemleri
Doktor işlemleri için gerekli metodları içerir.
![image](https://github.com/Ebruavsar/hospital/assets/73585933/82918e32-19e1-4595-bdc4-023a602049f9)

#### Sekreter İşlemleri
Sekreter işlemleri için gerekli metodları içerir.
![image](https://github.com/Ebruavsar/hospital/assets/73585933/35fde53d-134c-4ef2-8940-a64243e262d0)

#### Hasta İşlemleri
Hasta işlemleri için gerekli metodları içerir.
![image](https://github.com/Ebruavsar/hospital/assets/73585933/89b0f8fd-1ea2-4b0b-a193-b54ccca9cf49)

#### Randevu İşlemleri
Randevu işlemleri için gerekli metodları içerir.
![image](https://github.com/Ebruavsar/hospital/assets/73585933/dfc85cd2-30a7-4289-a646-e7405accca15)

#### Bölüme Göre Hasta Sayısı
Bölüme göre hasta sayısını hesaplar.
![image](https://github.com/Ebruavsar/hospital/assets/73585933/3305edfc-1b61-4361-9b96-1ea590e09de4)

#### Doktora Göre Hasta Sayısı
Doktora göre hasta sayısını hesaplar.
![image](https://github.com/Ebruavsar/hospital/assets/73585933/d2c86457-d3cc-44ae-ba18-9994bce0dc50)

### Doktor Giriş
DoktorGiris sınıfı, doktor kullanıcılarının sisteme giriş yapmasını sağlar.
![image](https://github.com/Ebruavsar/hospital/assets/73585933/a121808e-a57e-40f8-b485-ba2ba9f0eca2)

### Doktor
Doktor sınıfı, doktorların sistemde yapabileceği işlemleri içerir.
![image](https://github.com/Ebruavsar/hospital/assets/73585933/901af9c6-6c34-4dfd-bcf9-3472223402f2)

#### PDF
PDF dosyalarını yönetir.

#### E-Posta
E-posta gönderim işlemlerini yönetir.
![image](https://github.com/Ebruavsar/hospital/assets/73585933/3edaf0b8-e845-4f19-a162-333a4fff44db)

#### Randevu Geçmişi
Doktorun randevu geçmişini görüntüler.
![image](https://github.com/Ebruavsar/hospital/assets/73585933/4cfeda18-10c5-4608-9309-0b6513898bbc)

---

## Setup
Setup aşamasında gerekli kurulum adımları belirtilmiştir.

---

