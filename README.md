#Implementação do trabalho prático da disciplina de Sistemas Operacionais baseada no Algoritmo do Banqueiro, proposto por Silberschatz.

O objetivo do projeto é simular múltiplos clientes concorrentes solicitando e liberando recursos compartilhados, enquanto o sistema decide se cada solicitação pode ser atendida sem entrar em estado inseguro.

#Tecnologias utilizadas
- Linguagem: C#
- Plataforma: .NET
- Ambiente de execução: Windows / Visual Studio Code / Terminal

#Funcionalidades implementadas

- Criação de múltiplas threads de clientes
- Solicitações aleatórias de recursos
- Liberação aleatória de recursos
- Controle de concorrência com "lock"
- Verificação de estado seguro usando o Algoritmo do Banqueiro
- Negação de solicitações inseguras
- Exibição das operações no terminal

#Estruturas de dados utilizadas

- "available": recursos disponíveis
- "maximum": demanda máxima de cada cliente
- "allocation": recursos atualmente alocados
- "need": recursos ainda necessários

#Como compilar e executar
Pré-requisitos:
Instalar o .NET SDK.
Verificar instalação no terminal:
dotnet --version
Executar o projeto
#No terminal, dentro da pasta do projeto:

#dotnet run 10 5 7

Onde:

- "10" = quantidade do recurso A
- "5" = quantidade do recurso B
- "7" = quantidade do recurso C

#Exemplo de saída

Cliente 1 solicitou [1,0,2] -> APROVADO
Cliente 3 solicitou [3,1,0] -> NEGADO
Cliente 1 liberou [1,0,1]


