# Aplib.Net Demo Game (Fork)

> **This repository is a fork of [team-zomsa/aplib.net-demo](https://github.com/team-zomsa/aplib.net-demo).**
> 
> I forked this project to extend it with Unity ML-Agents for Reinforcement Learning experiments, as part of a thesis project.

Modifications include:
- Integration of ML-Agents package.
- Creation of training environments for RL experiments.
- Python training scripts located in `/RL_Training`.

For instructions on training the ML agent, see the [RL Training README](RL_Training/README.md)

## License
This project is licensed under the terms of the GNU Affero General Public License v3.0 (AGPL-3.0).
It is a fork of the [team-zomsa/aplib.net-demo](https://github.com/team-zomsa/aplib.net-demo) project, which is also licensed under AGPL-3.0.
The original LICENSE file has been preserved.

All modifications and additions in this fork, including the RL training scripts and the code integrating ML-Agents into the project, are licensed under the AGPL-3.0.

See [agpl-3.0.txt](./agpl-3.0.txt) for the full license text.

### Third-Party Licenses
This project uses the Unity ML-Agents Toolkit, which is licensed under the [Apache License 2.0](https://github.com/Unity-Technologies/ml-agents/blob/main/LICENSE.md).



The original README is included below for reference.

---

# Aplib.Net Demo Game
[![GitHub Release](https://img.shields.io/github/v/release/team-zomsa/aplib.net-demo?label=GitHub%20Release)
](https://github.com/team-zomsa/aplib.net-demo/releases)
[![GitHub Issues or Pull Requests](https://img.shields.io/github/issues/team-zomsa/aplib.net-demo)](https://github.com/team-zomsa/aplib.net-demo/issues)
[![GitHub Issues or Pull Requests](https://img.shields.io/github/issues-pr/team-zomsa/aplib.net-demo)](https://github.com/team-zomsa/aplib.net-demo/pulls)


## Overview
A demo game that demonstrates the capabilities of the [Aplib.Net library](https://github.com/team-zomsa/aplib.net), more information can be found on its repository.

The project makes use of Unity version 2022.3.19f1, meaning this is the version you will have to have installed to run the tests or build the game locally.
Unity can be downloaded using the [Unity Hub](https://unity.com/unity-hub).


## Testing
The Unity Testing Framework, used for running tests in Unity, knows two types of tests:
1. Edit mode tests, these run without running the entire game, and are defined as unit tests.
2. Play mode tests, these run the game and test the game's functionality as a whole, this is where the Aplib.Net library comes into play.

The tests, of which most importantly the play mode tests, that are used to demonstrate Aplib.Net, of the game are/can be run in two ways:
1. In the GitHub Actions pipeline, where testing is done, the Unity Testing Framework is used to run playmode tests. Aplib.Net tests are defined to be used in the UTF, and results are uploaded as an artifact. To view this, take a look at the [Actions tab](https://github.com/team-zomsa/aplib.net-demo/actions), and select a pipeline run where testing is performed, then press `Test Results`.
2. Locally, by opening the project in Unity and running the tests. This can be done by opening the Unity project, and navigating to the `Window` tab, then `General` and finally `Test Runner`. In the Test Runner, you can run all tests, or select specific tests to run. You will need to clone the repository to run the tests locally. Make sure to have the correct Unity version installed, as mentioned in the [Overview](#overview) section.


## Running the game
A build of the game can also be ran in multiple ways:
1. A WebGL build gets generated and uploaded to GitHub pages automatically, and can be visited here: [Aplib.Net Demo Game](https://team-zomsa.github.io/aplib.net-demo/).
2. The game can also be ran locally. Builds get generated automatically for Windows-64bit, Linux-64bit and OSX, these can be found in the [GitHub Release](https://github.com/team-zomsa/aplib.net-demo/releases).
3. The game can be built locally, by cloning the repository and opening the project in Unity, making sure to have the correct Unity version installed, as mentioned in the [Overview](#overview) section. Then go to the build settings, select the platform you want to build for, and press `Build`. The build settings menu can be opened by pressing `Ctrl + Shift + B` or by navigating to the `File` tab, then `Build Settings`.


## Contributing
This game is purely for demonstration purposes and thus does not accept any contributions.


## License
The project is provided under the GNU Affero General Public License, which can be found [here](https://github.com/team-zomsa/aplib.net-demo/blob/main/LICENSE).
