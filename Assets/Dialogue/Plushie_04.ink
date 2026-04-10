INCLUDE Globals.ink
EXTERNAL TableScene()
//Plushie 04 Sire Salvadore The 3rd
{MonkeyCollected == true: -> RegDia}
"Oh... You found monke... Well.."

"Kneel before thyself, I am Sire Salvadore The 3rd And thine shall do as i say, peasant!"
~MonkeyCollected = true
    ~plushies += 1
    {plushies == 6:
        ~TableScene()
        ->END
         - else:
        ->END
    }
    -> END

  ===RegDia===
  The TV doesn't work...
  -> END
  
