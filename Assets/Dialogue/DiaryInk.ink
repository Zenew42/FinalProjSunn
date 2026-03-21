INCLUDE Globals.ink
EXTERNAL DiaryScene()
//Diary

{free_time == true:
        ->DiaryAct2
    -else:
        ->DiaryAct1
}


//Act 1
===DiaryAct1===
Hahaha, that's my diary!!
You can't go through other people's diaries while they're watching, silly!
    ->END


//Act 2
===DiaryAct2===
You pick up your Friends' diary.
Are you sure you want to open it?

*[Yes]
    ~DiaryScene()
    ->DONE
*[No]
    ->END

//===Open===
//Your screen is covered with an image of an open book spread, the writing in the book spells out idfk creepy shit

//when you close the book, the friend is standing right behind you with their creepy sprite 
