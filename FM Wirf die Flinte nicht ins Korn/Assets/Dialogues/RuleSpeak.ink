VAR attack = 0
VAR roundattack = 0

VAR defense = 0
VAR winchance = 0

VAR roundCount = 0
VAR roundCountF = 0

VAR Ycounter = 0

VAR 5syl = "a"
VAR 7syl = ""


VAR rounds_won = 0
VAR rounds_lost = 0





okay, lass trinkon!

-> START

=== START ===

Die Regaln sind einfasch: #speaker:c

1. Wir beleidigon uns. 
Erscht funf Silbon, dann siebon,
dann wiedor funf. Dann wiedor von vorno.
2. Nasch drei Zeilon, kommt ein "Yo-'o-'o" (Yo-ho-ho)
und dann nasch zwei Zeilon 
ein "und na Budal voll Rum" (Und ne Budel voll Rum).
Dann wiedor von vorno.
3. Isch boginne und danasch immor die Gowinnorin.

Hascht du vorstandon?

+Narr, erklar nochmal! #speaker:f
    -> START
+Nochmal! [(Ohne Akzent)] #speaker:f
    -> START2
+Ayy, lass anfangen! #speaker:f
    -> ROUND_1R


-> ROUND_1R

=== START2 ===

1. Es wird begonnen mit einer Beleidigungen mit 5 Silben,
gefolgt wird mit einem Fluch mit 7 Silben,
darauf wieder eine mit 5 Silben. 
Dann wird wieder von vorne begonnen.
2. Nachdem drei Runden vergangen sind
(Jede Beleidigung gilt als eine Runde)
muss man statt der Beleidigung "Yo-ho-ho" sagen.
Zwei Runden nach dem "Yo-ho-ho" muss "Und ne Budel voll Rum
gesagt werden.
3. Die Gewinnerin fangt immer an.


+Narr, erklar nochmal!
    -> START
+Narr, erklar nochmal! [(Ohne Akzent)]
    -> START2
+Ayy, lass anfangen!
    -> ROUND_1R

=== ROUND_1R ===
~Ycounter +=1
~roundCount +=1
~winchance +=1
-> ATTACK_HANDLER



=== REGINA_CHECKER ===

{attack: 
- 1: Spiegel 'asson disch! #speaker:c
- 2: Die Schu'e stinkon! #speaker:c
- 3: Disch polier isch noch!#speaker:c
- 4: Et voilà la merde!#speaker:c
- 5: Du Leischtmatrosa!#speaker:c
- 6: Davy Jones' Kiste wartat schon! #speaker:c
- 7: Dein 'olzbein wird varfauart! #speaker:c
- 8: Du bischt ein raudigar 'und! #speaker:c
- 9: ubar die Planka mit dir! #speaker:c
- 10: Tu as un sacré culot! #speaker:c
- 11: Yo-'o-'o #speaker:c 
- 12: Yo-'o-'o #speaker:c  
- 13: Yo-'o-'o #speaker:c 
- 14: Yo-'o-'o #speaker:c 
- 15: Yo-'o-'o #speaker:c 
- 16: und na Budal voll Rum #speaker:c
- 17: und na Budal voll Rum #speaker:c
- 18: und na Budal voll Rum #speaker:c
- 19: und na Budal voll Rum #speaker:c
- 20: und na Budal voll Rum #speaker:c
}

{roundCount:
- 1: -> COUNT_HANDLER3
- 2: -> COUNT_HANDLER4
- else: -> COUNT_HANDLER5
}



=== CHOICES_R1 ===

{Ycounter == 3: -> COUNT_HANDLER1}
{Ycounter == 5: -> COUNT_HANDLER2 | -> CHOICES_R2 }


=== CHOICES_W ===

{Ycounter == 3: -> COUNT_HANDLER1}
{Ycounter == 5: -> COUNT_HANDLER2 | -> ROUND_WON}

=== CHOICES_R2 ===

~roundCount +=1
~Ycounter +=1

{roundCount:
- 1: -> COUNT_HANDLER6
- 2: -> COUNT_HANDLER7
- else: -> COUNT_HANDLER8
}


=== CHOICES_R3 ===

~5syl = RANDOM(1,5)
~7syl = RANDOM(1,5)

+ {5syl == 1} [5 Silben]
    Beim Klabautermann! #speaker:f
    ~defense = 1
    {Ycounter == 3: -> ROUND_LOST}
    {Ycounter == 5: -> ROUND_LOST}
    {roundCountF == defense: -> ROUND_1R | -> ROUND_LOST}
    
+ {5syl == 2} [5 Silben]
    Halt's Schandmaul, Sprotte! #speaker:f
    ~defense = 1
    {Ycounter == 3: -> ROUND_LOST}
    {Ycounter == 5: -> ROUND_LOST}
    {roundCountF == defense: -> ROUND_1R | -> ROUND_LOST}
    
+ {5syl == 3} [5 Silben]
    Versteck dich lieber! #speaker:f
    ~defense = 1
    {Ycounter == 3: -> ROUND_LOST}
    {Ycounter == 5: -> ROUND_LOST}
    {roundCountF == defense: -> ROUND_1R | -> ROUND_LOST}
    
+ {5syl == 4} [5 Silben]
    Verflucht sei dein Blut! #speaker:f
    ~defense = 1
    {Ycounter == 3: -> ROUND_LOST}
    {Ycounter == 5: -> ROUND_LOST}
    {roundCountF == defense: -> ROUND_1R | -> ROUND_LOST}
    
+ {5syl == 5} [5 Silben]
    Das Vieh gehort hinaus! #speaker:f
    ~defense = 1
    {Ycounter == 3: -> ROUND_LOST}
    {Ycounter == 5: -> ROUND_LOST}
    {roundCountF == defense: -> ROUND_1R | -> ROUND_LOST}
    
    
    
    
+ {7syl == 1} [7 Silben]
    Nimm den Stiefel aus dem Maul! #speaker:f
    ~defense = 2
    {Ycounter == 3: -> ROUND_LOST}
    {Ycounter == 5: -> ROUND_LOST}
    {roundCountF  == defense: -> ROUND_1R | -> ROUND_LOST}
    
+ {7syl == 2} [7 Silben]
    Pockiger Bilgenaffe! #speaker:f
    ~defense = 2
    {Ycounter == 3: -> ROUND_LOST}
    {Ycounter == 5: -> ROUND_LOST}
    {roundCountF  == defense: -> ROUND_1R | -> ROUND_LOST}  
    
+ {7syl == 3} [7 Silben]
    Dich werd ich Kiel holn lassen! #speaker:f
    ~defense = 2
    {Ycounter == 3: -> ROUND_LOST}
    {Ycounter == 5: -> ROUND_LOST}
    {roundCountF  == defense: -> ROUND_1R | -> ROUND_LOST}
    
+ {7syl == 4} [7 Silben]
    Bist den Kugeln zu schade! #speaker:f
    ~defense = 2
    {Ycounter == 3: -> ROUND_LOST}
    {Ycounter == 5: -> ROUND_LOST}
    {roundCountF  == defense: -> ROUND_1R | -> ROUND_LOST}
    
+ {7syl == 5} [7 Silben]
    Dei Visage schreckt nicht schlecht! #speaker:f
    ~defense = 2
    {Ycounter == 3: -> ROUND_LOST}
    {Ycounter == 5: -> ROUND_LOST}
    {roundCountF  == defense: -> ROUND_1R | -> ROUND_LOST}    
  
  
  
    
+ Yo-ho-ho! #speaker:f

    {Ycounter == 3: -> ROUND_1R}
    {Ycounter == 5: -> ROUND_LOST | -> ROUND_LOST}  


    
+ Und ne Budel voll Rum! #speaker:f

    {Ycounter == 3: -> ROUND_LOST}
    {Ycounter == 5: -> COUNT_HANDLER5Y | -> ROUND_LOST}







=== ATTACK_HANDLER ===
{Ycounter:
-3: {rando1()} 
-5: {rando2()} 
}
{Ycounter:
-3: -> REGINA_CHECKER
-5: -> REGINA_CHECKER
}
{winchance:
- 1: -> ATTACK_HANDLER1
- 2: -> ATTACK_HANDLER2
- else: -> ATTACK_HANDLER3
}



=== ATTACK_HANDLER1 ===

{roundCount:
- 1: -> ATTACK_SENDER1
- 2: -> ATTACK_SENDER2
-else: -> ATTACK_SENDER1
}

=== ATTACK_SENDER1 ==

~attack = RANDOM(1,5)

-> REGINA_CHECKER

=== ATTACK_SENDER2 ===

~attack = RANDOM(6,10)

-> REGINA_CHECKER







=== ATTACK_HANDLER2 ===

{roundCount:
- 1: -> ATTACK_SENDER3
- 2: -> ATTACK_SENDER4
- else: -> ATTACK_SENDER3
}


=== ATTACK_SENDER3 ===

~attack = RANDOM(1,7)

-> REGINA_CHECKER


=== ATTACK_SENDER4 ===

~attack = RANDOM(3,10)

-> REGINA_CHECKER






=== ATTACK_HANDLER3 ===

{roundCount:
- 1: -> ATTACK_SENDER5
- 2: -> ATTACK_SENDER6
- 3: -> ATTACK_SENDER5
}


=== ATTACK_SENDER5 ===


~attack = RANDOM(1,8)

-> REGINA_CHECKER


=== ATTACK_SENDER6 ===


~attack = RANDOM(5,10)

-> REGINA_CHECKER



=== ATTACK_HANDLERY1 ===
{rando1()} 
-> REGINA_CHECKER


=== ATTACK_HANDLERY2 ===
{rando2()} 
-> REGINA_CHECKER




=== function rando1() ===

~attack = RANDOM(11,16)
~return

=== function rando2() ===

~attack = RANDOM(15,20)
~return






=== COUNT_HANDLER1 === //Ycounter

{attack:
- 11: -> CHOICES_R2
- 12: -> CHOICES_R2
- 13: -> CHOICES_R2
- 14: -> CHOICES_R2
- 15: -> CHOICES_R2
- else: -> ROUND_WON
}

=== COUNT_HANDLER2 === //Ycounter

~Ycounter = 0
{attack:
- 16: -> CHOICES_R2
- 17: -> CHOICES_R2
- 18: -> CHOICES_R2
- 19: -> CHOICES_R2
- 20: -> CHOICES_R2
- else: -> ROUND_WON
}

=== COUNT_HANDLER3 === //5 Silben

{attack:
- 1: -> CHOICES_R1
- 2: -> CHOICES_R1
- 3: -> CHOICES_R1
- 4: -> CHOICES_R1
- 5: -> CHOICES_R1
- else: -> CHOICES_W
}


=== COUNT_HANDLER4 === //7 Silben

{attack:
- 6: -> CHOICES_R1
- 7: -> CHOICES_R1
- 8: -> CHOICES_R1
- 9: -> CHOICES_R1
- 10: -> CHOICES_R1
- else: -> CHOICES_W
}

=== COUNT_HANDLER5 === //5 Silben + roundCount Reset

~roundCount = 0

{attack:
- 1: -> CHOICES_R1
- 2: -> CHOICES_R1
- 3: -> CHOICES_R1
- 4: -> CHOICES_R1
- 5: -> CHOICES_R1
- else: -> CHOICES_W
}

=== COUNT_HANDLER6 ===

~roundCountF = 1

-> CHOICES_R3


=== COUNT_HANDLER7 ===

~roundCountF = 2

-> CHOICES_R3

=== COUNT_HANDLER8 ===

~roundCountF = 1
~roundCount = 0

-> CHOICES_R3


=== COUNT_HANDLER5Y ===

~Ycounter = 0

-> ROUND_1R


=== ROUND_WON ===

Haha, das war falsch!
Musst trinken! #speaker:f
Jetzt bin ich dran!
~rounds_won += 1
~Ycounter = 0
~roundCount = 0

{rounds_won == 5: -> STOP}

-> CHOICES_R1



=== ROUND_LOST ===

Ah oui, du musst trinkon! #speaker:c
Also, von vorn.
~rounds_lost += 1
~Ycounter = 0
~roundCount = 0

{rounds_lost == 5: -> STOP}

-> ROUND_1R



=== STOP ===

{rounds_won == 5: gewonnen! | verloren!}

->END