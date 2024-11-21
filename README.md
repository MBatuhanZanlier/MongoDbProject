## Açıklama: 
- Merhaba, bügün sizlere MongoDb nin .Net Core 6.0 ile kullanımını elimden geldiğince anlatacağım. Bir önceki projemde .Net Core 7.0  için anlatmıştım orada Orm olarak EntityFrameWork ile kullanmıştım ve detaylı bir şekilde anlatmıştım.Burada mongoDb nin .Net Core EntityFramework 7.0 altında bir sürümü olmadığı  için arasındaki farkları özetliyeceğim.
- Buraya .Net Core 7.0  EntityFrameWork ile geliştirdiğim projenin linkini bırakıyorum MongoDb hakkında bilgileri bulabilirsiniz.
- Link: https://github.com/MBatuhanZanlier/MongoDb_Villa_Project

## - .NET Core 6.0 ile MongoDb Kullanımı 

- İlk öncelikle bir tane Asp.Net Core Web App 6.0 ile  bir proje açınız. 

![Ekran Görüntüsü (266)](https://github.com/user-attachments/assets/2aac5739-d501-4467-a93b-9c91a27d722b)

- Tek Katmanda çalışacağız ve solide uymak için içinde olabildiğince klasör yapılarına ve isimlerine dikkat edeceğiz.
- projemize Oluşturduğumuz Klasörler
1. Entities: Burada Sınıflarımız olacak.
2. Dtos: Dtolarımızı tutacağız. , sadece ihtiyaca  propertylerimizi bulunduracak.
3. Mapping: Automapper işlemlerimizi gerçekleştireceğiz.
4. Services: İnterfacelerimizi ve Classlarımızı methodlarımızı tutacağız.
5. Settings: 
- Projemize MongoDb.Bson ve MongoDb.Driver kütüphanelerimizi yükliyelim.
  ##  Product ve Category sınıfların Yazılması
- ![Ekran Görüntüsü (268)](https://github.com/user-attachments/assets/22431a8c-77bd-4d59-bdba-e563f78bd961)
- Resimde görüldüğü gibi Propertylerimize attribute tanımladık  bunları açıklayalım.
  1. [BsonId]:
- Bu attribute, bir sınıfın özelliğini, MongoDB belgesinin benzersiz kimliği olarak işaret eder.
- MongoDB, her belgesini benzersiz bir _id alanına sahip olarak saklar. Bu alan genellikle ObjectId türünde olur.
- [BsonId] attribute, C# sınıfındaki hangi alanın MongoDB'nin _id alanına karşılık geleceğini belirtir.
  2. [BsonRepresentation(BsonType.ObjectId)]:
- Bu attribute, bir özelliğin veritabanında ObjectId türünde saklanmasını ama C# tarafında farklı bir türde (örneğin string) kullanılmasını sağlar.
- MongoDB'de _id alanı genellikle ObjectId türünde olur, ancak uygulama tarafında bu değeri bir string olarak işlemek isteyebilirsiniz.
- [BsonRepresentation(BsonType.ObjectId)] ile bu dönüşüm sağlanabilir.
 - ![Ekran Görüntüsü (269)](https://github.com/user-attachments/assets/2ca0cf82-4eac-41a8-b1a9-daebda28b6e1)
- Propertylerinizi  [BsonRepresentation(BsonType. )]  belirledikden sonra kısıtlayabilirsiniz.

## -Settings Klasorü 
![Ekran Görüntüsü (273)](https://github.com/user-attachments/assets/f6cf1568-bce5-477c-8a3f-830d214c5985)

Bu değerlerimizi Appsettings.jsonda  çağıracağız. Entityframeworkde 
- MongoDb de Tablolar Table olarak değil, Colleciton olarak geçtiği için Collection ismiyle isimlendiriyoruz. CategoryCollectionName  ile ProductCollectionName Collection isimlerine karşık geliyor. kolay anlaşılması için bu değerler EntityFramewrokde ki DbSetlerimize karşılık geliyor diyebiliriz.
- DatabaseName: Veri tabanı ismine karşılık geliyor olacak.
- ConnectionString: Bağlantı Adresimize karşılık geliyor Olacak.
![Ekran Görüntüsü (274)](https://github.com/user-attachments/assets/93836c9f-33c4-4804-acc3-990ff76bf47c)

## -Appsettings.json 
![Ekran Görüntüsü (272)](https://github.com/user-attachments/assets/1308dfeb-4e66-4655-a597-b17436932793) 
Settings içerisinde yazdığımız settingsleri appsettings.json içerisinde içlerini doldurduk.
## -Dtos ve Mapping Klasörleri 
- Burada ilk başta Dtos Sınıflarımızı Yazalım.
- ![Ekran Görüntüsü (275)](https://github.com/user-attachments/assets/75de05e9-fc74-46a4-80f8-814c2d42086a)
- AutoMapper Kütüphanemizi Projemize yükliyelim.
- ![Ekran Görüntüsü (276)](https://github.com/user-attachments/assets/67a2bc4c-2280-49a0-84b5-0e30416b2b82)
Product ve Category  sınıflarımızla Dtoslarımızı Mappleyelim.
## -services 
![Ekran Görüntüsü (278)](https://github.com/user-attachments/assets/2c692d69-f9c0-4d36-8b1e-71361aab0d7d)
- İnterfaceler içine Methodlarımızı yazalım,
- ![Ekran Görüntüsü (279)](https://github.com/user-attachments/assets/b883b0a8-0661-472a-a893-58a4f2717ab6)
- categoryService sınıfımız ICategoryServiceden miras alıyor.
- MongoDb nin Constructor ı biraz farklı olduğu için size burayı detaylı bir şekilde anlatmak istiyorum.
  -Ctor yazıp tab tuşuna basıyoruz ve countructoru  mauel olarak yazıyoruz.
  
- _categoryCollection: Bu, MongoDB veritabanındaki Category koleksiyonuna (collection) karşılık gelen koleksiyondur. Bu koleksiyon üzerinden verileri almak, eklemek, güncellemek gibi işlemler yapılacaktır.
- MongoClient: MongoDB veritabanına bağlanmak için kullanılan bir sınıftır. ConnectionString ise MongoDB'ye bağlanmak için kullanılan bağlantı dizesidir. Bu bağlantı dizesi IDataBaseSettings arayüzünden alınır.
- var database GetDatabase(): MongoDB istemcisine (client) bağlı bir veritabanı alır. Burada veritabanının adı, IDataBaseSettings'ten alınan DatabaseName özelliği ile sağlanır.
- categoryCollection GetCollection<T>(): Bu metot, belirli bir koleksiyonu (collection) almak için kullanılır. Burada _categoryCollection, MongoDB'deki Category koleksiyonuna erişir. Koleksiyonun adı, CategoryCollectionName özelliği ile sağlanır.
- Category, veritabanında saklanan belgelerin (documents) veri modelini temsil eder. Yani her kategori belgesinin sahip olacağı alanları (örneğin, Id, Name, vb.) içerir.
  ### -Özetle
  -Yaptğımızı özetliyecek olursak eğer;
1. MongoDB'ye Bağlanma: MongoClient ile veritabanına bağlanır.
2. Veritabanı Seçimi: GetDatabase() ile veritabanını alır.
3. Category Koleksiyonunu Seçme: GetCollection<Category>() ile ilgili koleksiyon seçilir.
4. IDatabaseSettingsi neden yazdığımızı şuan daha iyi anlaşıldığını düşünüyorum.
## -Methodlarımızın içlerini dolduralım 
-![Ekran Görüntüsü (281)](https://github.com/user-attachments/assets/69688c93-72ff-4f30-8cbf-af5712a08fa8)
-MongoDb nin methodları  bu şekilde CRUD methodlar üzerinden göstermeye anlatmaya çalıştım.  
## Program.cs 
![Ekran Görüntüsü (280)](https://github.com/user-attachments/assets/813557fb-d5ec-4213-bf82-7fd4b0795e08) 
-Program.cs tarafında config ayarları bu şekilde.  

## Okuduğunuz için Teşekür ederim. Elimden geldiğince açıklamak ve anlatmak istedim.


## MongoDb Google Cloud stroge Projesi 
### -Proje Hakkında 
- Bu projede Resimlerimiz MongoDb üzerine değilde Google Cloud stroge kaydedilmiştir ve Google Cloud stroge 'a kayıt edilen fotoğrafların yolu ise MongoDbye kayıt edilmiştir. Projede Ürünlerin Pdfi ve Müşteri bilgilerinin excelle indirme seçenekleri vardır.
## -Projede Kullanılan Teknolojiler ve Kütüphaneler 
- AutoMapper
- ClosedXML(excell şeklinde indirmek için)
- İTEXTSHARP(Pdfe indirmek için)
- MongoDB
- MongoDb.Bson
- MongoDb.driver
- Google Cloud Storge
  ### -Proje Fotoğrafları
#### -Category    
![Ekran Görüntüsü (253) - Kopya](https://github.com/user-attachments/assets/da2c2699-12f9-44af-8297-c494fa939d1e)
- Id yi bilerek bu şekilde bıraktım çünkü [BsonRepresentation(BsonType.ObjectId)]: string formatda olduğunu göstermek için.
  ## Müşteriler Ve Excell Formatında indirme
 - ![Ekran Görüntüsü (254)](https://github.com/user-attachments/assets/b91b83de-c215-4f64-bd6e-a689929205d2)
- ![Ekran Görüntüsü (257)](https://github.com/user-attachments/assets/28bd400b-6871-4741-bdfb-cc7bea85763a)
#### -Müşteriler ve PDF Formatında İndirme 
![Ekran Görüntüsü (258)](https://github.com/user-attachments/assets/e46a1031-7a1d-4837-b182-443fdca296ba)
![Ekran Görüntüsü (259)](https://github.com/user-attachments/assets/37d8ba84-d521-4c88-8686-96181860ce12)
![Ekran Görüntüsü (262)](https://github.com/user-attachments/assets/57ec85f9-70ba-44b2-a408-b299d800d427)
###  Google Cloud stroge 
-![Ekran Görüntüsü (264)](https://github.com/user-attachments/assets/249042d3-12f3-496b-ad1a-b525379b2a2a)
![Ekran Görüntüsü (263)](https://github.com/user-attachments/assets/51f7c36a-0094-4607-9525-0648909498c8)
