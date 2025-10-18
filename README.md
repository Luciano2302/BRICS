# BRICS

TAGS QUE  PRECISA CRIAR:
Tag	Para qual objeto
"Enemy"	✅ Todos os Bricks
"Player"	✅ Objeto Player
"Ball"	✅ Objeto Ball
"GameOver"	✅ WallBottom

MainScene
├── Main Camera
├── Canvas
│   ├── TextScore
│   ├── TextLives
│   └── TextLevel
├── GameManager (✅ AGORA ESTÁ NA CENA)
├── Player
├── Ball
├── Walls
│   ├── WallTop
│   ├── WallBottom (tag: GameOver)
│   ├── WallLeft
│   └── WallRight
└── Bricks (todos com tag: Enemy)