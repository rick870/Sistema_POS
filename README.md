# Sistema_POS

Backend para un sistema POS desarrollado con ASP.NET Core (.NET 8) y Arquitectura en Capas.

---

## ☁️ Despliegue

La API se encuentra desplegada en Microsoft Azure.


https://web-pos-api-rafael-fxbjdxf6drcggjc2.westcentralus-01.azurewebsites.net/swagger/index.html

---
TOKEN:

Usuario: cajero / 
Password: cajero123

---

⚠️ Nota para usuarios de ESET NOD32 / Antivirus estrictos: > > Debido a que el entorno está desplegado en un subdominio gratuito y genérico de pruebas de Azure (.azurewebsites.net), algunos antivirus pueden marcar la URL temporalmente como un falso positivo de phishing por su longitud. El sitio es 100% seguro; si te aparece la advertencia, puedes hacer clic en "Ignorar amenaza / Continuar al sitio" para acceder al panel de Swagger normalmente.

---

## 🧩 Arquitectura del Proyecto

El sistema está dividido en múltiples capas:

- `POS.Api` → API y controladores
- `POS.Application` → lógica de negocio
- `POS.Domain` → entidades y dominio
- `POS.Infrastructure` → acceso a datos
- `POS.Utilities` → utilidades
- `POS.Test` → pruebas

---

## ⚙️ Tecnologías Utilizadas

- ASP.NET Core Web API (.NET 8)
- Entity Framework Core
- SQL Server
- JWT Authentication
- Azure App Service
- Azure SQL Database

---




## 📌 Características

- Arquitectura en capas
- Autenticación JWT
- Manejo de productos y ventas
- API REST
- Integración con Azure

---



## 👨‍💻 Autor

Agustin Gonzales
