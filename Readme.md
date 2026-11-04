# Braille Image Classifier (Backpropagation Neural Network)

A Windows Forms application (C#, Visual Studio) that reads a picture of a single braille cell and tells you which letter (A to Z) it is. The recognition is done by a 3-layer neural network (input, hidden, output) trained with backpropagation, using the neural network classes provided in class (`InputN`, `HiddenN`, `OuputN`, `BackNeural`).


## Dataset

- Source: [Braille Character Dataset (Kaggle, shanks0465)](https://www.kaggle.com/datasets/shanks0465/braille-character-dataset)
- 26 classes (letters a to z), 28 x 28 pixel images, 1,560 images in total (60 per letter)
- The images are augmented versions (dim, rot, whs in the file names) of the same 26 base characters.
- The dataset is included in this repository. 

### Preprocessing

1. Read each image and resize it to 28 x 28 (784 pixels).
2. Scale every pixel to the range 0 to 1 (divide by 255). This keeps the sigmoid neurons out of saturation, so the network can learn.
3. The letter becomes a one-hot target: 26 outputs, 1 for the correct letter and 0 for the others.
4. Split into training and test sets (see Split below).
5. Optionally save everything to `braille.csv` (letter, type, 784 pixel values) so it can be reloaded quickly.

## How it works

```
784 inputs (pixels)  ->  H hidden neurons (sigmoid)  ->  26 outputs (sigmoid)
```

For every training image the network:

1. **Forward pass:** computes the hidden and output activations.
2. **Error:** target minus output for each of the 26 outputs.
3. **Backward pass:** multiplies each error by `output x (1 - output)`, passes it back to the hidden neurons through the weights.
4. **Update:** each weight changes by `learning rate x error signal x incoming value` (biases are updated too).

One pass through the whole training set is one **epoch**. The training order is shuffled every epoch.

**Loss** for one image is `0.5 x sum over the 26 outputs of (target - output)^2`, averaged over all images in the set. An untrained network outputs about 0.5 everywhere, so the loss starts near 3 and should fall as training goes on.

**Accuracy** is the share of images where the largest output is the correct letter. Note that the 26 outputs are independent sigmoids, not probabilities, so they do not add up to 1. The app therefore shows the raw output values (for example `A 0.93`), not percentages.

## Project structure

| File | What it does |
|---|---|
| `BrailleData.cs` | Reads the images, normalizes pixels, saves/loads the CSV, splits train/test, shuffles. |
| `NetHelper.cs` | Connects the data to the network: sets inputs and targets, runs the network, computes loss, accuracy, confusion matrix, precision and recall. |
| `Form1.cs` | Main screen: upload an image and interpret it. |
| `Form2.cs` | Trainer window: load data, set hyperparameters, train, evaluate, run experiments. |


## Navigation

The main screen can only interpret images after the network has been trained, so you start with the Trainer.

### A. Train the network

1. **Click `Trainer`** on the main screen. The training window opens.
2. **Click `Build CSV`** and choose the folder that contains the braille images. The app reads every image, shows how many images and letters it found (expected: 1560 images, 26 letters) and saves `braille.csv`. You only need this once. Later, use **`Load CSV`** to load it faster.
3. **Choose the settings** (see "Settings explained"). A good starting point: Split = Random 80/20, Hidden = 64, Epochs = 100, LR Out = 0.2, LR In = 0.15.
4. **Click `Train`.** Every 10 epochs the log shows the train loss, test loss and test accuracy, and the graph draws the train loss (blue) and test loss (red). When it prints "Finished", the weights are saved automatically. **`Stop`** ends training early.
5. **Click `Evaluate`** to see the overall accuracy and the precision and recall of every letter. A `confusion_matrix.csv` file is saved.
6. **Try the hold-out splits** (Hold out: dim, rot, whs) and train again to see how the network handles variations it has not seen.
7. **Click `Run Experiments`** to test every combination of hidden size (64, 128, 256) and learning rate (0.02, 0.05, 0.1). The results are added to `experiments.csv`. This takes a while.
8. **Train one last time with the best settings** you found. The last training run is the network the main screen will use.

### B. Interpret an image

9. **Close the trainer.** The main screen loads the newly saved weights.
10. **Click `Upload`** and choose an image of a braille cell.
11. **Click `Interpret`.** The big letter is the network's answer, and below it are the three highest outputs, for example `Top 3: A 0.93   C 0.12   B 0.05`.

## Settings explained

| Setting | Meaning |
|---|---|
| **Split** | Which images the network learns from and which it is tested on. *Random 80/20* takes 80% of each letter for training and 20% for testing. *Hold out dim / rot / whs* trains on the other two augmentation types and tests on the held-out one. |
| **Hidden** | Number of neurons in the hidden layer. More neurons can learn more complex patterns but train slower and can memorize the training images. |
| **Epochs** | How many times the network goes through the whole training set. Too few: it has not learned enough. Too many: it starts to memorize (train loss keeps falling while test loss stops improving). |
| **LR Out** | Learning rate (step size) for the weights between the hidden layer and the outputs. |
| **LR In** | Learning rate for the weights between the pixels and the hidden layer. |

A learning rate that is too high makes the loss jump around or never settle. One that is too low makes learning very slow.

## Output files

All files are written to the folder of the executable (`bin\Debug\...`).

| File | Content |
|---|---|
| `braille.csv` | The preprocessed dataset (letter, type, 784 pixel values). |
| `training_log.csv` | Epoch, train loss, test loss and test accuracy (logged every 10 epochs). |
| `experiments.csv` | Results of Run Experiments: hidden size, learning rates, epochs, test loss, test accuracy. |
| `confusion_matrix.csv` | 26 x 26 table of real letter (rows) versus predicted letter (columns). |
| `braille_weights.txt`, `braille_hidden.txt` | The trained weights and the hidden size they belong to (used by the main screen). |

## EVALUATION 


**Chosen configuration:** Hidden = 128, LR Out = .05, LR In = 0.0375, Epochs = 200

**Why:** 


## Limitations

- Recognizes one braille cell (letters a to z) per image. It does not read whole words or pages, numbers, capital signs or punctuation.
- Any uploaded image is resized to 28 x 28, so it works best with images that look like the dataset.

## Current Configurations for Current Best Accuracy (72.12%)

Epoch: 200
Hidden: 128
LROut: 0.05
LRIn: 0.0375

-------------------------------

- Dataset: Braille Character Dataset by shanks0465 on Kaggle
- Neural network classes: Chris Jordan Aliac
-  John Lawrence T. Rebusit | Mary Rose T. Pacina
