import os
import subprocess
import argparse

def main():
    parser = argparse.ArgumentParser(description="Train an ML-Agent.")
    parser.add_argument(
        "--config", 
        type=str, 
        default="configs/your_training_config.yaml", 
        help="Path to the training configuration file."
    )
    parser.add_argument(
        "--run-id", 
        type=str, 
        default="default_run", 
        help="The run ID for this training session."
    )
    args = parser.parse_args()

    # Build the command
    command = [
        "mlagents-learn",
        args.config,
        "--run-id", args.run_id,
        "--env", None,  # None means "connect to running Unity editor"
    ]

    print(f"Running training with config: {args.config} and run ID: {args.run_id}")
    
    # Launch the training
    subprocess.run([c for c in command if c is not None])

if __name__ == "__main__":
    main()