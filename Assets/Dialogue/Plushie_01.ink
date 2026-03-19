INCLUDE Globals.ink
EXTERNAL TableScene()
//plushie 01 Mikkel the fox

"Hehehe"
"You found Mikkel!!"
//fox talking
"Hai I'm Mikkel The Fox, Noice to meit you Fraind"
    ~plushies += 1
    {plushies == 6:
        ~TableScene()
        else:
        ->END
    }
    -> END
