# 🎮 HouseVania - Symphony of the Day

**Autores:** Grégory Fortunato e Yuri Vaz Claro  
**Engine:** Unity 2D  
**Linguagem:** C#  
**Data:** Junho de 2025

---

## 📑 Sumário

1. [Introdução](#1-introdução)  
2. [Tecnologias Utilizadas](#2-tecnologias-utilizadas)  
3. [Arquitetura do Projeto](#3-arquitetura-do-projeto)  
4. [Scripts Principais e Funcionalidades](#4-scripts-principais-e-funcionalidades)  
5. [Sistema de Dificuldade](#5-sistema-de-dificuldade)  
6. [IA de Inimigos e Boss](#6-ia-de-inimigos-e-boss)  
7. [Controle de Vida e Dano](#7-controle-de-vida-e-dano)  
8. [Animações e Efeitos Sonoros](#8-animações-e-efeitos-sonoros)  
9. [Considerações Finais e Melhorias Futuras](#9-considerações-finais-e-melhorias-futuras)  
10. [Galeria de Imagens](#10-galeria-de-imagens)

---

## 1. Introdução

*HouseVania - Symphony of the Day* é um jogo de ação 2D com combate corpo a corpo e inimigos com comportamento inteligente.  
O jogador pode se mover, pular, agachar e atacar. O jogo possui:

- Sistema de dificuldade dinâmica
- Inimigos variados
- Chefe (boss) com ataques normais e especiais

---

## 2. Tecnologias Utilizadas

- **Unity** (versão sugerida: 2022.x.x LTS)
- **Linguagem:** C#
- **Bibliotecas e Componentes:**
  - Sistema de animação da Unity
  - Rigidbody2D / Collider2D para física
  - AudioSource para sons
  - Delegates e eventos personalizados

---

## 3. Arquitetura do Projeto

### 📂 Estrutura Geral

- **Player:** Scripts de movimento e ataque  
- **Enemies:** Boss, Ghost, HeavyBandit  
- **Sistema de Vida:** `HealthSystem.cs`  
- **Sistema de Dificuldade:** `GameDifficultyManager.cs`  
- **Utilidades:** CameraAimTarget, HitBoxes, Controladores de dano

### 🧠 Padrões Utilizados

- **Singleton:** para o `GameDifficultyManager`
- **Event Delegates:** comunicação entre `HealthSystem` e animações
- **Modularidade:** cada inimigo possui script com comportamento específico

---

## 4. Scripts Principais e Funcionalidades

- **`PlayerMovement.cs`:** Movimento, pulo, agachamento, animações e sons.
- **`PlayerAttack.cs` + `HitBoxAttack.cs`:** Sistema de ataque com hitbox temporária.
- **`HurtAnimationController.cs`:** Controla animação de dano quando o jogador é atingido.
- **`HealthSystem.cs`:** Controla a vida, aplica dano e dispara eventos de morte.

---

## 5. Sistema de Dificuldade

- **`GameDifficultyManager.cs`:** Singleton que define os parâmetros:
  - Dano
  - Velocidade
  - Tempo de recarga
- **`DifficultySelector.cs`:** Tela inicial para seleção de dificuldade e início do jogo.

---

## 6. IA de Inimigos e Boss

- **`Boss.cs`:**
  - Ataques normais e especiais com cooldowns
  - Detecção de distância
  - Uso de `UpdateLookDirection()` para virar
  - Ataques especiais com efeitos visuais (magia/partículas)

- **`HeavyBandit.cs`:**
  - Persegue o jogador
  - Ataca quando próximo

- **`GhostAttack.cs` + `GhostMove.cs`:**
  - Dash
  - Retorno
  - Movimento flutuante

---

## 7. Controle de Vida e Dano

Todos os personagens usam `HealthSystem.cs`, garantindo:

- Reutilização do código
- Disparo de eventos para animações
- Modularidade para lógica de morte e efeitos

---

## 8. Animações e Efeitos Sonoros

- **Áudio:**
  - AudioSource para passos (loop)
  - AudioSource para SFX (ataque, pulo)
- **Animações:**
  - Triggers para ataque, pulo, dano e morte
  - Eventos de animação sincronizam ações (ex: ativar/desativar hitbox)
- **Feedback visual e sonoro:**  
  Reação responsiva e sincronizada com ações do jogador/inimigos.

---

## 9. Considerações Finais e Melhorias Futuras

### ✅ Pontos Fortes

- Modularidade dos scripts
- Sistema de dificuldade dinâmico
- Inimigos com comportamentos únicos
- Mecânicas principais estáveis

### 🔧 Melhorias Futuras

- Sistema de **respawn** ou **checkpoints**
- Implementação de **HUD** com vida e energia
- Balanceamento com base em testes
- Menu de **pausa com opções**
- Sistema de som mais robusto com **Audio Mixer**


