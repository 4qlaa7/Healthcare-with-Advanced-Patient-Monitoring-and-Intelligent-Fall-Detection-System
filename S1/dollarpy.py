import cv2
import struct
from dollarpy import Recognizer,Template,Point
import numpy as np
import mediapipe as mp
import time


def Send_farmes(img_str):
    global isok
    if isok:
        global conn
        conn.sendall(struct.pack("Q", len(img_str)))   
        conn.sendall(img_str)



def send_video(path):
    global isok  
    while True:
        if isok:
            print("sending ok")
            global conn
            video_file = open(path, "rb")
            video_data = video_file.read(1024)
            while video_data:
                conn.send(video_data)
                video_data = video_file.read(1024)
            video_file.close()
            print("sent")


def count_cameras():
    num_cameras = 0
    camera_index = 0

    while True:
        cap = cv2.VideoCapture(camera_index)
        if not cap.isOpened():
            break
        num_cameras += 1
        cap.release()
        camera_index += 1
    return num_cameras

numofcam=count_cameras()
camindex=0
cap = cv2.VideoCapture(camindex)
def cameraread():
    while True:
        # Read a frame from the camera
        ret, frame = cap.read()    
        # Encode the frame as JPEG
        encode_param = [int(cv2.IMWRITE_JPEG_QUALITY), 90]
        _, img_encoded = cv2.imencode('.jpg', frame, encode_param)
        img_str = img_encoded.tobytes()
        Send_farmes(img_str)


mp_drawings = mp.solutions.drawing_utils
mp_holistic = mp.solutions.holistic



def get_points(videopath,label):
    cap = cv2.VideoCapture(videopath)
    with mp_holistic.Holistic(min_detection_confidence=0.7,min_tracking_confidence=0.7) as holistic:
        points = []
        left_shoulder = []
        right_shoulder = [] 
        left_elbos = []
        right_elbos = []
        left_wrist = []
        right_wrist = []
        left_pinky = []
        right_pinky = []
        left_index = []
        right_index = []
        left_hip = []
        right_hip = []

        while cap.isOpened():
            ret,frame = cap.read()

            if ret == True :
                image = cv2.cvtColor(frame,cv2.COLOR_BGR2RGB)

                results = holistic.process(image)

                image = cv2.cvtColor(frame,cv2.COLOR_RGB2BGR)

                #right hand
                mp_drawings.draw_landmarks(
                        image,results.right_hand_landmarks,mp_holistic.HAND_CONNECTIONS,
                        mp_drawings.DrawingSpec(color=(80,22,10),thickness=2,circle_radius=4),
                        mp_drawings.DrawingSpec(color=(80,44,121),thickness=2,circle_radius=2),

                )

                #left hand
                mp_drawings.draw_landmarks(
                        image,results.left_hand_landmarks,mp_holistic.HAND_CONNECTIONS,
                        mp_drawings.DrawingSpec(color=(121,22,76),thickness=2,circle_radius=4),
                        mp_drawings.DrawingSpec(color=(121,44,250),thickness=2,circle_radius=2),

                )

                #pose Detection

                mp_drawings.draw_landmarks(
                        image,results.pose_landmarks,mp_holistic.HAND_CONNECTIONS,
                        mp_drawings.DrawingSpec(color=(121,22,76),thickness=2,circle_radius=4),
                        mp_drawings.DrawingSpec(color=(121,44,250),thickness=2,circle_radius=2),

                )

                try:
                    pose = results.pose_landmarks.landmark
                    index = 0
                    list=[]

                    for lnd in pose:
                        if index in [11,12,13,14,15,16,17,18,19,20,23,24]:
                            list.append(lnd)
                        index+=1
                    

                    # add points to lists

                    left_shoulder.append(Point(list[0].x,list[0].y,1))
                    right_shoulder.append(Point(list[1].x,list[1].y,2))
                    left_elbos.append(Point(list[2].x,list[2].y,3))
                    right_elbos.append(Point(list[3].x,list[3].y,4))
                    left_wrist.append(Point(list[4].x,list[4].y,5))
                    right_wrist.append(Point(list[5].x,list[5].y,6))
                    left_pinky.append(Point(list[6].x,list[6].y,7))
                    right_pinky.append(Point(list[7].x,list[7].y,8))
                    left_index.append(Point(list[8].x,list[8].y,9))
                    right_index.append(Point(list[9].x,list[9].y,10))
                    left_hip.append(Point(list[10].x,list[10].y,11))
                    right_hip.append(Point(list[11].x,list[11].y,12))

                except:
                    pass
                cv2.imshow(label,image)
            if cv2.waitKey(10) & 0xFF == ord('q'):
                break
    
    cap.release()
    cv2.destroyAllWindows()
    points = left_shoulder+right_shoulder+left_elbos+right_elbos+left_wrist+right_wrist+left_pinky+right_pinky+left_index+right_index+left_hip+right_hip
    print(label)
    return points



# Fall Backwad
def traindata():
    Temps = []
    i = 1
    while i < 3:
        vid = f"Dataset/Fall_Backward/S{i}.avi"
        pointt = get_points(vid,"Fall_Backward")
        tmpl = Template('Fall_Backward',pointt)
        Temps.append(tmpl)
        i+=1

    # Fall Forward
    i = 1
    while i < 3:
        vid = f"Dataset/Fall_Forward/S{i}.avi"
        pointt = get_points(vid,"Fall_Forward")
        tmpl = Template('Fall_Forward',pointt)
        Temps.append(tmpl)
        i+=1


    # Fall_Left
    i = 1
    while i < 3:
        vid = f"Dataset/Fall_Left/S{i}.avi"
        pointt = get_points(vid,"Fall_Left")
        tmpl = Template('Fall_Left',pointt)
        Temps.append(tmpl)
        i+=1


    # Fall_right
    i = 1
    while i < 3:
        vid = f"Dataset/Fall_right/S{i}.avi"
        pointt = get_points(vid,"Fall_right")
        tmpl = Template('Fall_right',pointt)
        Temps.append(tmpl)
        i+=1


    # Fall_sitting
    i = 1
    while i < 3:
        vid = f"Dataset/Fall_sitting/S{i}.avi"
        pointt = get_points(vid,"Fall_sitting")
        tmpl = Template('Fall_sitting',pointt)
        Temps.append(tmpl)
        i+=1
    return Temps





def test(Temps):
    vidd = "Dataset/Fall_right/S7.avi"
    pointt = get_points(vidd,"sherif_elo5if")
    print(pointt)
    start = time.time()
    recognizer = Recognizer(Temps)
    result = recognizer.recognize(pointt)
    end = time.time()
    duration = end - start
    print(result)
    print(duration)
    return result