# OpenCV Animations
Trabalho realizado na disciplina de animação computadorizada, pela professora Rossana Queiroz. Unisinos/RS.

![Imagem de capa](cover.png)

## Objetivo

Este trabalho buscou criar uma animação em um personagem atravéz dos movimentos de braços e pernas de um vídeo caseiro, utilizando visão computacional para identificar as partes de uma pessoa em cada frame do vídeo.

## Dependências
Como temos 2 softwares, devemos ter os requisitos instalados para ambos os processos.

**opencv-animation-mediapipe**
Primeiramente você deve ter um interpretador python 3 e o gerenciador de pacotes pip instalados e setados como variáveis de ambiente em sua máquina.

```shell
cd ./opencv-animation-mediapipe
# Instalação das dependências do projeto
pip install opencv-python
pip install mediapipe
# Adicione os seus videos desejados com a extensão .mp4
# Para executar o projeto
python ./main.py
```

**opencv-animations-unity**
Você deve ter a Unity engine instalada em sua máquina (esse projeto foi feito com a versão 2021.3.9), em seguida, basta adicionar o projeto ao unityhub, e após abri-lo, apenas execute como um jogo comum.

## Funcionamento

Basta inserir qualquer vídeo com a extensão mp4, dentro da pasta /opencv-animation-mediapipe, o software identifica todos esses videos, e ao executar python main.py dentro desse diretório, o script cria um arquivo "animation_i.json" na área de trabalho, um para cada vídeo inserido no repositório.
Com os vídeos gerados, basta importar o projeto opencv-animations-unity dentro da Unity Engine, e dar play na plataforma, se ao menos 3 arquivos de animations foram gerados na área de trabalho, o sistema deve reproduzir as animações sem erros.