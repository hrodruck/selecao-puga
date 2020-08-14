Projeto de teste feito como parte da seleção para PUGA studios



Tarefa 1: As funcionalidades foram adicionadas em UI Manager, o qual é componente de Managers na cena Test. O parâmetro Time to Win define o tempo da partida.

Tarefa 2: O novo inimigo se chama STAY_SAFE. Seu comportamento é de se manter distanciado do jogador e atirar. Ele não dá dano, assim como alguns outros dos nove inimigos pré-prontos. Creio que seja bug na parte do código recebido que trata do dano dos inimigos.

Tarefa 3: A definição do Scriptable Object se encontra na pasta "Assets/Scripts/Scriptable Objects". Se chama ShipStatus.

Tarefa 4: O fluxo é feito a partir do Scene Manager, script separado em objeto vazio (por conta do DontDestroyOnLoad)

Tarefa 5: Foi utilizada a classe PlayerPrefs. A quantidade de moedas é salva a cada fim de jogo (quer seja vitória ou derrota), mas não durante o jogo. Isto é, se o jogador fechar o jogo no meio, suas moedas não serão contabilizadas. Acredito que seja comportamento esperado.

Tarefa bônus: a lógica de passar os dados de escolha entre as cenas está em Scene Manager, a lógica de escolher está em Home Custom Picker, ambos na cena Home.

Pontos de destaque:

A tela e a UI se ajustam bem a várias resoluções, para dar suporte à futura versão Android. Assumi que seria usado o formato landscape quando fiz a tela de Home.

Corrigi o bug prévio que impedia o jogador de morrer sob stun

Toda a navegação utiliza botões já cadastrados nas configurações de Input, e não o mouse, para maior suporte a joystick

O armazenamento da quantidade de moedas coletadas é independente de plataforma


Qualquer ajuste ou má compreensão minha, peço que me retorne em hrodruck@gmail.com


