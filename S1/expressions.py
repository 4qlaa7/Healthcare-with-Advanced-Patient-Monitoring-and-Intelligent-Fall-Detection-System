import cv2
import matplotlib.pyplot
from deepface import DeepFace




def expression():
    DicOfEmotn = {}
    vid = cv2.VideoCapture(0)
    frameCounter = 0

    while vid.isOpened:
        ret,frame = vid.read()
        frameCounter +=1
        if ret:
            results = DeepFace.analyze(frame,actions = ['emotion'],enforce_detection=False)
            emotion = results[0]['dominant_emotion']
            if bool(DicOfEmotn) is False:
                DicOfEmotn.update({f"{emotion}": 1})
            else:
                if emotion in DicOfEmotn:
                    key = DicOfEmotn[f"{emotion}"]
                    key += 1
                    DicOfEmotn.update({f"{emotion}": key})
                else:
                    DicOfEmotn.update({f"{emotion}": 1})
                if frameCounter == 50:
                    break
        else:
            break
        max_value = max(DicOfEmotn, key=DicOfEmotn.get)
        return max_value


print(expression())

