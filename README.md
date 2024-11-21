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
