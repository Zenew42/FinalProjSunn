INCLUDE Globals.ink
Welcome to my party Friend!!

You should put on a party hat so we can match!

They are right over there~

Just walk over with "WASD" and press "E" to interact ^-^
->END

//Player can walk over and put on a hat


Now that everyone is here... let's sit down!

//characters sit at the table

Since it's my birthday we should play a game!!
Which game do you wanna play friend?

* [I don't know.]
-> FriendDecides

* [You decide]
-> FriendDecides


===FriendDecides===
Oh.. okay hm...
-> Question1

===Question1===
Let's play Would you rather!

I'll start

Would you rather eat strawberries or chocolate?

// this later decides the cake flavour
* [Strawberries]
-> Strawberries

* [Chocolate]
-> Chocolate


===Strawberries===
Yummy!!! i love strawberries aswell hehe!
->Question2

===Chocolate===
Chocolate is my favourite too hehe!!
->Question2

===Question2===
Next question!!

Hmmmm...

You liiiike me righttt???

* [Yes]
->YouLike
* [You're cool]
->DontLike
* [You're OK]
->DontLike
* [I don't know you well..]
->DontLike

===YouLike===
I Knew you liked me hehe!!
->Question3

===DontLike===
hahah silly you! of course you like me!
->Question3

===Question3===
Moving on!

Let's see...
If you could have a pet... Which would you have?
 //Could be brought up later in a creepy way, maybe a dead animal or smth lol
* [Dog]
->Cute
* [Cat]
->Cute
* [Bunny]
->Cute
* [Snake]
->Scary
* [A rock]
->Funny

===Cute===
Awww.. that would be such a cute pet! ^^
->Question4
===Scary===
Realllyyy!! You're so brave... I'm too scared of snakes... :o
->Question4
===Funny===
Hehehehe! You're so funny!!
->Question4


===Question4===
Last Question!!

Would you like to spend the rest of your life with me?

* [What?]
->Question5
* [Huh..?]
->Question5
* [...]
->Question5

===Question5===
Umm... Your turn to ask questions!! heheh

* [Can i leave now?]
->CakePrep


===CakePrep===
Why would you want to leave now?? It's storming outside!
//Thunder sound effect and light flickering followed by sound of rain and storm 
Anyway! It's time for cake! I'll go prep it hehe! ^^

//free time


-> END
