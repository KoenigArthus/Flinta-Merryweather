Regina? #speaker:f

Oui? #speaker:c

-> CHOICES

== CHOICES == 

+ [Piratengilde] #speaker:f
    -> GILDE

+ [Schlaegerei] #speaker:f
    -> TS

+ [Trinkspiel] #speaker:f
    -> TRINKSPIEL

+ [Tschuess] #speaker:f
    -> BYE

== GILDE ==

Du hast was ueber die Piratengilde gesagt… #speaker:f
Weißt du wie ich ihr beitreten kann?

Asch, due weischt das nischt? #speaker:c
uend due willst Pirataenkoenigin waerdaen?

Wir fangen ja alle irgendwo an #speaker:f

C'est frai #speaker:c
Die Pirataengildae koemmt auf disch zu, waenn due einaen Ruef aufgaebaut 'ast

Einen Ruf als Piratenkapitaen? #speaker:f

Oui, daer laetztae, woe ihnaen beigaetraetaen ischt, 'at einae ganzae  Floettae daer Navy ausgaetrickt #speaker:c
uend ein Kroenjuewael diraekt uentaer i'raer Nasae gaeklaut

Das ist ja kinderleicht #speaker:f

Oui oui, bien sur #speaker:c

-> CHOICES

== TS ==

Wenn ich eine Schlaegerei hier lostrete, dann kommst du in meine Crew?

Oui! #speaker:c
Mischae daen Ladaen 'ier rischtig auf uend isch foelgae dir woe'in due willst

-> CHOICES


== TRINKSPIEL == 

Ich hab gehoert, du spielst gerne diese Spiel...

Oh, non non non. Diesae Zeit ischt voebei! #speaker:c

Haette dich ja eh unter den Tisch getrunken #speaker:f

'oer mal haer kleinae... #speaker:c

Ist schon okay. Bist ja schießlich nicht mehr die Juengste! #speaker:f

Sacre Bleu. Dir zeig ischs! #speaker:c

+ [Muss mich doch noch vorbereiten]
    -> BYE

+ [FLINTENDIALOG]
    -> WARNUNG

== WARNUNG ==

> Der Flintendialog beendet das Kapitel. Es gibt kein zurueck mehr. 
> Wenn du anfaengst bekommst du je nach deinen Entscheidungen einen anderen Ausgang des Dialogs
> In diesem Kapitel gibt es 3 Gegenstaende, die dich hierbei vorbereiten koennen:
> Fischbroetchen, Riechsalz, Trinktechnik
> Ohne die Items wird der Dialog deutlich schwerer!

+ [FLINTENDIALOG anfangen?] #state:flintendialog
    -> END
    
+ [Lieber nochmal vorbereiten]
    Gute Entscheidung
    -> END
    
== BYE ==

Warte nur, ich ueberzeuge dich schon noch 

Bitte lassch dir nischt zue viel Zeit #speaker:c
aes ist soenst langweilig

-> END






