INCLUDE Globals.ink
EXTERNAL TableScene()
//Plushie 04 Sire Salvadore The 3rd

"Oh... You found monke... Well.."

"Kneel before thyself, I am Sire Salvadore The 3rd And thine shall do as i say, peasant!"
    ~plushies += 1
    {plushies}
    {plushies == 6:
        ~TableScene()
         - else:
        ->END
    }
    -> END
