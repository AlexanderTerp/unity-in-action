# Unity in Action [2nd Edition] - Alexander Terp's Implementation

Unity in Action is a textbook written by Joseph Hocking.

Goodreads link: https://www.goodreads.com/book/show/23816485-unity-in-action

It teaches Unity through various mini-projects. This repository contains my implementations of those projects. Some parts will contain the code practically 1:1, others can be very different.

## Part 1 - Super Basic FPS

A very basic "FPS" with a CharacterController and randomly moving AI which shoots a slow-moving projectile at the player when they're in front of the AI. The player has a hit-scan ability to shoot dead these AI.

There's a second scene which contains a little level of walls and a burning bench. The bench is an imported 3D Unity asset, the fire is a projectile system using my custom drawn "fire" particles (below, truly top quality!)

<img src="./media/fire.png" width="50">

## Part 2 - 2D Memory Game

A simple 2D implementation of "the memory game" i.e. faced with a grid of top-down cards, reveal two at a time to match. If matching, leave up, if not matched, face-down again. Continue until all cards revealed.

Involved drawing some more ~~bad~~ great pixel art for the card symbols.

<img src="./media/tree.png" width="40"> <img src="./media/sword.png" width="40"> <img src="./media/shield.png" width="40"> <img src="./media/face.png" width="40"> <img src="./media/horse.png" width="40"> <img src="./media/crown.png" width="40">