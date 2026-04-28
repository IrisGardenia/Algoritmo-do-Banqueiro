*Algoritmo do Banqueiro*

Implementação do trabalho prático da disciplina de Sistemas Operacionais utilizando o Algoritmo do Banqueiro, baseado na obra de Silberschatz, Galvin e Gagne.

---

*Linguagem e tecnologias utilizadas*

- Linguagem: C#
- Framework: .NET
- Sistema Operacional: Windows
- Ambiente recomendado: Visual Studio Code ou terminal

---

*Pré-requisitos*

Para executar o projeto é necessário instalar o .NET SDK.

Verifique se está instalado com o comando:

dotnet --version

Se aparecer a versão instalada, o ambiente está pronto.

---

*Como compilar e executar*

Abra o terminal dentro da pasta do projeto e execute:

dotnet run 10 5 7

*Significado dos parâmetros*

- "10" = quantidade do recurso A
- "5" = quantidade do recurso B
- "7" = quantidade do recurso C

Esses valores inicializam o vetor "available".

---

*Objetivo do projeto*

Simular múltiplos clientes concorrentes solicitando e liberando recursos compartilhados, enquanto o sistema decide se cada solicitação pode ser atendida sem entrar em estado inseguro.

---

*Funcionalidades implementadas*

- Criação de múltiplas threads de clientes
- Solicitações aleatórias de recursos
- Liberação aleatória de recursos
- Controle de concorrência com "lock"
- Verificação de estado seguro usando o Algoritmo do Banqueiro
- Negação de solicitações inseguras
- Exibição das operações no terminal

---

*Estruturas de dados utilizadas*

- "available": recursos disponíveis
- "maximum": demanda máxima de cada cliente
- "allocation": recursos atualmente alocados
- "need": recursos ainda necessários

---

*Exemplo de saída*

Cliente 1 solicitou [1,0,2] -> APROVADO
Cliente 3 solicitou [3,1,0] -> NEGADO
Cliente 1 liberou [1,0,1]

---

Referência bibliográfica

SILBERSCHATZ, Abraham; GALVIN, Peter B.; GAGNE, Greg. Fundamentos de Sistemas Operacionais. 9. ed. LTC.