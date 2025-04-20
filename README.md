# Proje Hakkında

Birbirleri ile haberleşen minimum iki microservice'in olduğu bir yapıdır. Basit bir telefon rehberi uygulaması ve konuma göre telefon rehberinin raporlanması sağlanmıştır. 

# Çalıştırma ve Kullanımı

Proje docker ortamında docker compose ile çalışabilmektedir.

<code>docker compose up</code> : Bu komutu proje ana dizini içerisinde çalıştırarak projeyi ayağa kaldırabilirsiniz.

<code>docker compose down</code> : container'ları silmek için kullanabilirsiniz. Image'lar silinmez.

API Contact Service Endpoint (CONTACT_SERVICE_ENDPOINT) : <code>https://localhost:5001/</code>

API Report Service Endpoint (REPORT_SERVICE_ENDPOINT) : <code>https://localhost:5101/</code>

<h3>Contact Endpoints</h3>

Kişi listeleme: [HttpGet] [CONTACT_SERVICE_ENDPOINT]/api/Persons/GetPersons

Kişi detayları: [HttpGet] [CONTACT_SERVICE_ENDPOINT]/api/Persons/GetPerson

Kişi ekleme: [HttpPost] [CONTACT_SERVICE_ENDPOINT]/api/Persons/CreatePerson

Kişi silme: [HttpDelete] [CONTACT_SERVICE_ENDPOINT]/api/Persons/DeletePerson

İletişim bilgisi ekleme: [HttpPost] [CONTACT_SERVICE_ENDPOINT]/api/Persons/CreateContactInformation

İletişim bilgisi silme: [HttpDelete] [CONTACT_SERVICE_ENDPOINT]/api/Persons/DeleteContactInformation

<h3>Report Endpoints</h3>

Rapor listeleme: [HttpGet] [REPORT_SERVICE_ENDPOINT]/api/Reports/GetReports

Rapor detayları: [HttpGet] [REPORT_SERVICE_ENDPOINT]/api/Reports/GetReport

Rapor talep oluşturma: [HttpPost] [CONTACT_SERVICE_ENDPOINT]/api/Reports/CreateReport

# Teknik Detaylar

Sistemde asenkron iletişim Masstransit kütüphanesi ve RabbitMQ ile yapılmaktadır. Eventlerin produce ve consume edilmesin de Outbox ve Inbox pattern kullanılmıştır. Contact ve Report servislerin de PostgreSQL veritabanı kullanılmıştır. Her iki servis için ayrı ayrı veritabanı oluşturulmuştur. Servislerin gelişime açık ve esnek bir mimari olması için Clean Architecture uygulanmıştır. Her bir serviste unit testler XUnit ile yazılmıştır.

Kullanılan teknolojiler

<ul>
  <li>.Net Core</li>
  <li>PostgreSQL</li>
  <li>Git</li>
  <li>RabbitMQ</li>
  <li>Entity Framework Core</li>
  <li>Dapper</li>
  <li>FluentValidation</li>
  <li>MediatR</li>
  <li>MassTransit</li>
  <li>Quartz</li>
  <li>Scrutor</li>
  <li>Mapster</li>
  <li>FluentAssertions</li>
  <li>NSubstitute</li>
</ul>

Kullanılan Design Patternler

<ul>
  <li>Repository</li>
  <li>Unit Of Work</li>
  <li>CQRS</li>
  <li>Outbox ve Inbox Pattern</li>
</ul>