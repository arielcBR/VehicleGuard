# 🚗 VehicleGuard

Sistema distribuído de monitoramento veicular com inteligência contextual. O VehicleGuard vai além do rastreamento passivo — ele cruza a localização do veículo com a do proprietário, aplica regras de anomalia e permite intervenção imediata pelo app mobile.

> ⚠️ **Projeto educacional em desenvolvimento ativo.** Documentação e funcionalidades em evolução.  
> 📖 Acompanhe as decisões de arquitetura no blog: [Parsed — Diário de um Dev em Transição](https://hashnode.com/@arielcBR)

---

## 🧠 O Problema

O monitoramento veicular tradicional é **passivo e reativo**: registra onde o carro esteve, mas não te dá poder de agir. O VehicleGuard resolve três pontos críticos:

- **Alertas inteligentes** — cruzamento de geolocalização do veículo com a do celular do proprietário, evitando falsos positivos por ruído de GPS
- **Autonomia de ação** — o proprietário pode bloquear a bomba de combustível ou acionar a buzina remotamente, de forma manual ou por regras automáticas pré-configuradas
- **Confiabilidade** — confirmação de leituras consecutivas antes de qualquer ação crítica, proteção contra dados obsoletos

---

## 🏗️ Arquitetura

```
┌─────────────────────────────────────────────────────────────┐
│                        VehicleGuard                         │
│                                                             │
│  [ESP32 + GPS + Acelerômetro]                               │
│         │ MQTT (rede celular)                               │
│         ▼                                                   │
│  [Broker MQTT - Mosquitto]                                  │
│         │                                                   │
│    ┌────┴────┐                                              │
│    │         │                                              │
│    ▼         ▼                                              │
│  [API .NET]  [Worker Service .NET]  ──► [SQL Server]        │
│    │         │ (parsing, regras de negócio, alertas)        │
│    │         │                                              │
│    └────┬────┘                                              │
│         │ REST + FCM (Push Notifications)                   │
│         ▼                                                   │
│  [App Mobile - React Native]                                │
└─────────────────────────────────────────────────────────────┘
```

**Estratégia de desenvolvimento:** o hardware (ESP32) é simulado via **Node-RED** enquanto o backend está sendo construído, isolando variáveis e garantindo que cada camada seja validada de forma independente.

---

## 🗂️ Estrutura da Solution

O projeto adota um **Monorepo .NET** com múltiplos projetos sob uma única Solution:

```
VehicleGuard.sln
├── VehicleGuard.Api          # API REST - endpoints, autenticação, controle de acesso
├── VehicleGuard.Worker       # Background Service - escuta MQTT, parsing, alertas
└── VehicleGuard.Shared       # Contratos compartilhados - Domain, DTOs, Interfaces
```

> A decisão de usar Monorepo ao invés de repositórios separados ou Git Submodules está documentada no post [Do curso do Balta.io ao Monorepo](https://hashnode.com/@arielcBR).

---

## 🛠️ Stack

| Camada | Tecnologia |
|---|---|
| Backend | C# / .NET 10 |
| Background Service | Worker Service (.NET) |
| Mensageria | MQTT (Mosquitto) |
| Banco de dados | SQL Server + Entity Framework Core |
| App Mobile | React Native |
| Notificações | Firebase / FCM |
| Hardware (futuro) | ESP32, GPS, Acelerômetro |
| Mock de Hardware | Node-RED |

---

## 🚦 Status atual

- [x] Estrutura da Solution (Monorepo)
- [x] Projeto Shared com Domain, DTOs e Interfaces
- [ ] API REST — em desenvolvimento
- [ ] Worker Service (consumer MQTT)
- [ ] Integração com broker Mosquitto
- [ ] App React Native
- [ ] Hardware ESP32

---

## 📖 Blog da série

As decisões de arquitetura, os problemas enfrentados e as escolhas feitas ao longo do desenvolvimento estão documentadas no blog:

- [Hello world — a motivação do projeto](https://hashnode.com/@arielcBR)
- [O plano inicial e a arquitetura do VehicleGuard](https://hashnode.com/@arielcBR)
- [Do tracker_api ao Monorepo — a primeira decisão de arquitetura](https://hashnode.com/@arielcBR)

---

## 👤 Autor

**Ariel Campos**  
[![LinkedIn](https://img.shields.io/badge/LinkedIn-campos--ariel-blue?style=flat&logo=linkedin)](https://www.linkedin.com/in/campos-ariel/)
[![GitHub](https://img.shields.io/badge/GitHub-arielcBR-black?style=flat&logo=github)](https://github.com/arielcBR)
