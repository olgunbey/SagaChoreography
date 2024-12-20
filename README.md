# Saga Pattern
### Event/Choreography
- Burada mikroservisler, haberleşmek için eventleri kullanarak haberleşir. Servisler arası eventler message brokerlar sayesinde taşınır.
- Burada her servis kendi kararını verir (Başarılı veya başarısız durumlarında hangi servisle iletişime geçeceği mantığı).
- Transaction esnasında her servis muhakkak başarılı başarısız bilgisini kuyruğa ekler, ve ilgili servis veya servisler haberdar olur.
- Fazla servislerde bu implementasyon tercih edilmemeli
- Transaction yönetimi merkezi olmadığı için performance bootleneck azdır

![MicroservicePlan](https://github.com/user-attachments/assets/d456deba-2e62-4534-9ab4-823270e7d725)


Yukarıdaki yapıda kırmızı ile işaretlenenler fail, yeşil ile işaretlenenler başarılı senaryolardır.

# Event List
### Publish
- OrderCreated 
- PaymentCanceled

### Send
- StockAccepted
- StockCanceled
- PaymentCanceled

  



