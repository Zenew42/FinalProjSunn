INCLUDE Globals.ink
EXTERNAL TableScene()
//Window and plushie


{FoxCollected == true: 
        -> RegDia
    -else:
        ->NotCollectedFox
        }

===NotCollectedFox===
"Hehehe"
"You found Mikkel!!"
//fox talking
"Hai I'm Mikkel The Fox, Noice to meit you Fraind"
    ~plushies += 1
    ~FoxCollected = true
    {plushies == 6:
        ~TableScene()
    -else:
        ->END
    }
    -> END

===RegDia===
{current_act:
1: ->WindowAct1
2: ->WindowAct2
-else: error
}

//act 1 freetime 1
===WindowAct1===
"The weather outside is so nice!! Hehehe
Perfectly Sunny for a perfect birthday"
    ->END



//Act 2 freetime 2
===WindowAct2===
It's Storming
    -> END
