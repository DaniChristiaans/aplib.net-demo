# RL Training Scripts

This folder contains the Python scripts and configuration files for training reinforcement learning (RL) agents using Unity ML-Agents.

> **Note:** This RL training code is part of the [Aplib.Net Demo Game Fork](../README.md).

---
## Setup Instructions

### Requirements
- Python **3.9.13**
- Unity ML-Agents Toolkit **0.26.0**
- PyTorch **1.8.0** (choose your installation based on your machine: CUDA 11.1, CUDA 10.2, or CPU-only)

1. **Create a Python virtual environment:**

   ```bash
   cd RL_Training
   python -m venv venv
   venv\Scripts\activate
   ```

### 2. Install PyTorch manually

Depending on your hardware setup:

- **CUDA 11.1 (for RTX 30xx GPUs or newer)**

  ```bash
  pip install torch==1.8.0+cu111 torchvision==0.9.0+cu111 torchaudio==0.8.0 -f https://download.pytorch.org/whl/torch_stable.html
  ```

- **CUDA 10.2 (for GTX 16xx/20xx GPUs)**

  ```bash
  pip install torch==1.8.0 torchvision==0.9.0 torchaudio==0.8.0
  ```

- **CPU-only (no GPU or for maximum compatibility)**

  ```bash
  pip install torch==1.8.0+cpu torchvision==0.9.0+cpu torchaudio==0.8.0 -f https://download.pytorch.org/whl/torch_stable.html
  ```

---

### 3. Install the remaining dependencies

```bash
pip install -r requirements.txt
```

---

### 4. Start training

```bash
mlagents-learn configs/your_training_config.yaml --run-id=your_run_id
```

---

### 5. Connect to Unity

- Open the Unity project (`aplib.net-demo`) in Unity Editor.
- Press `Play` to start the environment.
- The training will automatically connect.

---

## Folder Structure

| Folder/File | Description |
|:---|:---|
| `configs/` | Training configuration files (`.yaml`) for ML-Agents. |
| `requirements.txt` | List of Python dependencies. |
| `train.py` (optional) | Your custom Python script to launch or manage training. |

---

## 📊 Monitoring Training Progress

You can monitor the agent’s learning progress live using **TensorBoard**.

TensorBoard reads the training summaries that ML-Agents writes automatically during training.

### 1. Start TensorBoard

After you start training with `mlagents-learn`, open a **new terminal** (don't stop training!) and run:

```bash
tensorboard --logdir results
```

- `results/` is the folder where ML-Agents saves the training logs.
- TensorBoard will start and print a local URL (usually `http://localhost:6006`).
- Open the URL in your web browser.

- Now you can see a bunch of interesting graphs.

---

### 2. Example Workflow

| Step | Command |
|:---|:---|
| Start training | `mlagents-learn configs/your_training_config.yaml --run-id=your_run_id` |
| Open a second terminal | |
| Start TensorBoard | `tensorboard --logdir results` |
| Open browser | Go to `http://localhost:6006` |

For training with imitation learning (BC and/or GAIL), mlagents expects a folder with demo recordings, made inside your environment using the mlagents demo recording component.
When recording to the default directory inside the Unity project, the training command requires some modifications, and needs to be run from a different directory.

1. After activating the venv in RL_Training, use `cd ../aplib.net-demo` to navigate to the directory [GitHub projects]\aplib.net-demo\aplib.net-demo - the Unity project folder.
2. From there, run `mlagents-learn ../RL_Training/configs/jump_agent_config.yaml --run-id=NormalCam2Jumps`

Now mlagents-learn can find the demo folder in Assets/Demonstrations, and it can find the training config in ../RL_Training/configs/your_trianing_config.yaml.

---

## License

All RL training scripts and modifications are licensed under the GNU Affero General Public License v3.0 (AGPL-3.0).

This project uses the Unity ML-Agents Toolkit, which is licensed under the [Apache License 2.0](https://github.com/Unity-Technologies/ml-agents/blob/main/LICENSE.md).
