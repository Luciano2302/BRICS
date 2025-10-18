🎮 BRICK BREAKER - Documentação Completa
📋 ÍNDICE
Configuração Inicial

Estrutura de Cenas

Tags Necessárias

Configuração de Audio

Scripts e Componentes

Solução de Problemas

⚙️ CONFIGURAÇÃO INICIAL
Pré-requisitos
Unity 2021.3+ recomendado

Projeto 2D configurado

Resolução: 1920x1080 (recomendado)

Estrutura de Pastas Recomendada
text
Assets/
├── Scripts/
│   ├── Managers/
│   ├── MVC/
│   └── UI/
├── Audio/
│   ├── SFX/
│   └── Music/
├── Scenes/
├── Prefabs/
└── Materials/
🎯 ESTRUTURA DE CENAS
1. MenuScene - Tela Inicial
Objetos Necessários:

Canvas

StartButton (Button)

GameTitle (Text)

ObjectiveText (Text)

2. MainScene (Level 1) - Cena Principal
Hierarchy:

text
MainScene
├── Main Camera
├── Canvas
│   ├── TextScore
│   ├── TextLives  
│   └── TextLevel
├── GameManager (objeto vazio com script)
├── Player (tag: "Player")
├── Ball (tag: "Ball") 
├── Walls
│   ├── WallTop (tag: "Wall")
│   ├── WallBottom (tag: "GameOver") ⭐ CRÍTICO
│   ├── WallLeft (tag: "Wall")
│   └── WallRight (tag: "Wall")
└── Bricks (todos com tag: "Enemy")
3. Level2 - Segundo Nível
Mesma estrutura do MainScene

9 bricks (tag: "Enemy")

4. GameOverScene - Tela de Game Over
Objetos Necessários:

Canvas

RestartButton (Button)

QuitButton (Button)

FinalScoreText (Text)

5. WinnerScene - Tela de Vitória
Objetos Necessários:

Canvas

PlayAgainButton (Button)

QuitButton (Button)

FinalScoreText (Text)

🏷️ TAGS NECESSÁRIAS
Crie estas tags no Unity:

Tag	Para qual objeto	Importância
"Enemy"	✅ Todos os Bricks	⭐ CRÍTICO
"Player"	✅ Objeto Player	⭐ CRÍTICO
"Ball"	✅ Objeto Ball	⭐ CRÍTICO
"GameOver"	✅ WallBottom	⭐ CRÍTICO
"Wall"	✅ Outras paredes	⭐ CRÍTICO
Como Criar Tags no Unity:
Inspector → selecione qualquer objeto

Tag dropdown → Add Tag...

+ (plus button) → adicione cada tag

Aplique as tags aos objetos correspondentes

🎵 CONFIGURAÇÃO DE AUDIO
1. Preparação dos Arquivos
Formatos suportados: .wav, .mp3, .ogg

Estrutura recomendada:

text
Assets/Audio/
├── SFX/
│   ├── brick_destroy.wav
│   ├── player_collision.wav
│   ├── level_complete.wav
│   ├── game_over.wav
│   └── victory.wav
└── Music/
    └── background_music.mp3
2. Configuração do AudioManager
Crie um objeto vazio chamado "AudioManager"

Adicione o script AudioManager.cs

No Inspector, configure:

Sound Effects:

Brick Destroy - som quando brick é destruído

Player Collision - som quando bola bate no player

Level Complete - som ao completar nível

Game Over - som de game over

Victory - som de vitória

Background Music:

Background Music - música de fundo (loop)

3. Configuração das Propriedades
Para SFX (Efeitos Sonoros):

Load Type: Decompress on Load

Compression Format: PCM

Load in Background: false

Para Música:

Load Type: Streaming

Compression Format: Vorbis

Load in Background: true

📜 SCRIPTS E COMPONENTES
🎮 GameManager - Cérebro do Jogo
Responsabilidades:

Gerenciar estado do jogo (score, vidas, nível)

Transições entre cenas

Controle de UI

Sistema de audio

Configuração:

Objeto deve ter DontDestroyOnLoad

Configurar referências de UI no Inspector

Conectar eventos de audio

🏓 Sistema MVC da Bola
BallModel (Dados):

Speed: velocidade da bola

Direction: direção atual

Power: poder (para futuras expansões)

BallView (Visual/Controles):

Detecta colisões

Aplica física

Corrige ângulos problemáticos

Mantém velocidade mínima

BallController (Lógica):

Calcula reflexões

Gerencia colisões com diferentes objetos

Comunica com GameManager

🧱 Sistema MVC dos Bricks
BrickModel:

Health: vida do brick (geralmente 1)

BrickView:

Recebe dano

Comunica com controller

BrickController:

Aplica dano

Destroi brick quando health <= 0

Notifica GameManager

🎮 Sistema do Player
PlayerModel:

Speed: velocidade de movimento

Life: vidas (não utilizado atualmente)

Scale: escala do objeto

PlayerView:

Detecta input do teclado

Movimenta o player

PlayerController:

Aplica movimento

Limita movimento dentro da tela

🔧 SOLUÇÃO DE PROBLEMAS
❌ NullReferenceException no GameOverManager
Problema: Botões não encontrados na cena
Solução:

Verifique os nomes dos objetos no Canvas:

Deve ser RestartButton

Deve ser QuitButton

Deve ser FinalScoreText

Ou use a versão com FindUIReferences()

❌ Bricks não são contados corretamente
Problema: Level completa antes de destruir todos os bricks
Solução:

Verifique se todos os bricks têm tag "Enemy"

Verifique se não há bricks com Health <= 0 no início

GameManager faz limpeza automática na inicialização

❌ Bola fica presa/travada
Solução:

BallView tem sistema de correção de ângulos

Velocidade mínima configurada em 8

Sistema de "escape" se bola colar nas paredes

❌ Transição entre níveis com vidas erradas
Solução:

GameManager restaura vidas para 3 em cada novo nível

Score é mantido entre níveis

❌ Audio não funciona
Solução:

Verifique se AudioManager existe na cena

Confirme se arquivos de audio estão atribuídos

Verifique volumes (não devem ser 0)

Confirme se formatos de arquivo são suportados

🎯 CONFIGURAÇÃO RÁPIDA - CHECKLIST
✅ Antes de Executar Pela Primeira Vez:
Tags criadas: Enemy, Player, Ball, GameOver, Wall

Cenas configuradas com objetos corretos

GameManager em todas as cenas de jogo

AudioManager na cena inicial

Walls com colliders e tags corretas

Bricks com tag "Enemy" e Health > 0

✅ Para Cada Nova Cena de Nível:
Copiar estrutura do MainScene

Manter nomes de objetos consistentes

Garantir que todos os bricks têm tag "Enemy"

WallBottom com tag "GameOver"

🚀 COMO TESTAR
Inicie pelo MenuScene

Teste colisões:

Bola x Brick → deve destruir brick e tocar som

Bola x Player → deve refletir e aplicar penalidade

Bola x WallBottom → deve perder vida

Complete o Level 1 → deve ir para Level2

Complete o Level 2 → deve ir para WinnerScene

Perda de todas vidas → deve ir para GameOverScene

📞 SUPORTE
Problemas Comuns:

Tags não aplicadas → NullReferenceException

Nomes de objetos diferentes → botões não funcionam

Audio files faltando → sem som

Build settings → cenas não incluídas

Para Importar em Novo Computador:

Importe todos os assets

Siga o checklist de configuração

Crie as tags necessárias

Configure o AudioManager com arquivos de audio

🎮 Seu Brick Breaker está pronto para jogar! 🚀