# WPFLandmarkAI
A WPF application capable of identifying landmarks using Microsoft's Custom Vision AI (www.customvision.ai).

The app has a trained model that is capable of identifying images of the Eiffel Tower, the Golden Gate bride, and the Temple of Kukulcán.
When an image of one of the aforementioned locations is uploaded, a call to the Custom Vision API is made and the probability of each possible classification is displayed.
This is not a perfect classifier as can be seen from the example of the Moon and the mountains shown in the gif below.

![](LandmarksAIExampleGif.gif)
