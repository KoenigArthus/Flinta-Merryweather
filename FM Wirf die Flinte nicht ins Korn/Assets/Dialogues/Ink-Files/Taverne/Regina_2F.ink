Regina? #speaker:f

Oui? #speaker:c

-> CHOICES

== CHOICES == 

+ [Piratengilde] #speaker:f
    -> GILDE

+ [Schlägerei] #speaker:f
    -> TS

+ [Trinkspiel] #speaker:f
    -> TRINKSPIEL

+ [Tschüss] #speaker:f
    -> BYE

== GILDE ==

Du hast was über die Piratengilde gesagt… #speaker:f
Weißt du wie ich ihr beitreten kann?

Asch, dü weischt das nischt? #speaker:c
Ünd dü willst Piratänkönigin wärdän?

Wir fangen ja alle irgendwo an #speaker:f

C’est frai #speaker:c
Die Piratängildä kömmt auf disch zu, wänn dü einän Rüf aufgäbaut ‘ast

Einen Ruf als Piratenkapitän? #speaker:f

Oui, där lätztä, wö ihnän beigäträtän ischt, ‘at einä ganzä  Flöttä där Navy ausgätrickt #speaker:c
ünd ein Krönjüwäl diräkt üntär i’rär Nasä gäklaut

Das ist ja kinderleicht #speaker:f

Oui oui, bien sur #speaker:c

-> CHOICES

== TS ==

Wenn ich eine Schlägerei hier lostrete, dann kommst du in meine Crew?

Oui! #speaker:c
Mischä dän Ladän ‘ier rischtig auf ünd isch fölgä dir wö’in dü willst

-> CHOICES


== TRINKSPIEL == 

Ich hab gehört, du spielst gerne diese Spiel...

Oh, non non non. Diesä Zeit ischt vöbei! #speaker:c

Hätte dich ja eh unter den Tisch getrunken #speaker:f

'ör mal här kleinä... #speaker:c

Ist schon okay. Bist ja schießlich nicht mehr die Jüngste! #speaker:f

Sacre Bleu. Dir zeig ischs! #speaker:c

+ [Muss mich doch noch vorbereiten]
    -> BYE

+ [FLINTENDIALOG]
    -> WARNUNG

== WARNUNG ==

> Der Flintendialog beendet das Kapitel. Es gibt kein zurück mehr. 
> Wenn du anfängst bekommst du je nach deinen Entscheidungen einen anderen Ausgang des Dialogs
> In diesem Kapitel gibt es 3 Gegenstände, die dich hierbei vorbereiten können:
> Fischbrötchen, Riechsalz, Trinktechnik
> Ohne die Items wird der Dialog deutlich schwerer!

+ [FLINTENDIALOG anfangen?] #state:flintendialog
    -> END
    
+ [Lieber nochmal vorbereiten]
    Gute Entscheidung
    -> END
    
== BYE ==

Warte nur, ich überzeuge dich schon noch 

Bitte lassch dir nischt zü viel Zeit #speaker:c
Äs ist sönst langweilig

-> END






