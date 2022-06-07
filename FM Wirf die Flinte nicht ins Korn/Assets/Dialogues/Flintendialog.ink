VAR drunk = 0
VAR win = 0
VAR insultattack = 0
VAR insultdefense = 0

VAR attack = 0
VAR defense = 0


VAR score = false

Give me your <color=green>grrrrr</color> #speaker:f

~insultattack = RANDOM(0,3)

Let's play a Game #speaker:c

-> INSULT_REGINA


== INSULT_REGINA == 

My Turn! #speaker:c

{insultattack == 0: A} 
{insultattack == 1: B} 
{insultattack == 2: C} 
{insultattack == 3: D} 

+a #speaker:F                 
    ~insultdefense = 0
    {insultattack == insultdefense: -> WON_INSULT | -> LOST_INSULT } 
+b #speaker:F                 
    ~insultdefense = 1
    {insultattack == insultdefense: -> WON_INSULT | -> LOST_INSULT } 
+c  #speaker:F                
    ~insultdefense = 2
    {insultattack == insultdefense: -> WON_INSULT | -> LOST_INSULT } 
+d  #speaker:F               
    ~insultdefense = 3
    {insultattack == insultdefense: -> WON_INSULT | -> LOST_INSULT } 



== INSULT_FLINTA ==

My Turn! #speaker:f

+A
    ~attack = 0
    ~defense = RANDOM(0,3)
+B
    ~attack = 1
    ~defense = RANDOM(0,3)
+C
    ~attack = 2
    ~defense = RANDOM(0,3)
+D
    ~attack = 3
    ~defense = RANDOM(0,3)
--> RESPONSE




== RESPONSE ==

#speaker:c
{defense == 0: a }
{defense == 1: b }
{defense == 2: c }
{defense == 3: d }

{defense == attack:  -> LOST_INSULT | -> WON_INSULT }


== WON_INSULT ==

Drink up! #speaker:F
    ~win += 1
    
{win == 5: -> WIN}

--> INSULT_FLINTA



== LOST_INSULT ==

Drink up! #speaker:C
    ~drunk += 1

{drunk == 5:  -> LOSE} 

--> INSULT_REGINA




== LOSE ==

lost #state:loser

-> END


== WIN ==

won #state:winner

-> END