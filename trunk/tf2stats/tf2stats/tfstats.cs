/*
Every Line starts with:

#define [START] = L MM/DD/YYYY - HH:mm:ss: 

ALL users adhere to the following specification:

#define [USER] = "username<#><STEAM_ID><TEAM>"


Connect Specification:

[START] [USER]  connected, address "#.#.#.#:#"

Validate Specification:

[START] [USER] STEAM USERID validated

Join Team Specification:

[START] [USER] joined team "[TEAM]"

Change Character:

[START] changed role to "[character]"

Kill Specification:

[START] [USER] killed [USER] with "[WEAPON]" (attacker_position "# # #") (victim_position "# # #")

Assist Specification:

[START] [USER] triggered "kill assist" against [USER] (assister_position "# # #") (attacker_position "# # #") (victim_position "# # #")

Build Specification:

[START] [USER] triggered "builtobject" (object "[OBJECT]") (position "# # #")


enum weapon
{
	sniper,
}

enum object
{
}

class User
{
	string username;
	string steamid;
	string team;
	int num;
}
*/