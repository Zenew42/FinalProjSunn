INCLUDE Globals.ink
EXTERNAL TableScene()
//plushie 01 Mikkel the fox

{FoxCollected == true: -> RegDia}


"Hehehe"
"You found Mikkel!!"
//fox talking
"Hai I'm Mikkel The Fox, Noice to meit you Fraind"
    ~plushies += 1
    ~FoxCollected = true
    {plushies == 6:
        ~TableScene()
        - else:
        ->END
    }
    -> END

===RegDia===
while
-> END