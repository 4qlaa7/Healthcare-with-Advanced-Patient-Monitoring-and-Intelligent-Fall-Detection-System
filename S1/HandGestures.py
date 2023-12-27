from ultralytics import YOLO
import cv2
import numpy as np
from collections import Counter
import socket

def reco(conn):
    model = YOLO("Best_Model.pt")
    camera = cv2.VideoCapture(0)
    frame_counter = 0
    num_frames_per_batch = 20
    captured_frames = []
    while True:
        ret, frame = camera.read()

        if not ret:
            print("Failed to grab frame")
            break

        cv2.imshow("test", frame)
        captured_frames.append(frame)
        frame_counter += 1

        if frame_counter == num_frames_per_batch:
            # Predict using the captured frames
            predicted_classes = []
            results = model(captured_frames,conf=0.4)  # list of Results objects -> perform inference using the model
            names = model.names

            for r in results:
                for c in r.boxes.cls:
                    predicted_classes.append(names[int(c)])

            # Find the most frequent predicted class
            most_common_class = Counter(predicted_classes).most_common(1)

            # Check if the most_common_class list is not empty before accessing its elements
            if most_common_class:
                final_predicted_class = most_common_class[0][0]
                msg=("GESTURE,"+str(final_predicted_class))
                msg = bytes(final_predicted_class, 'utf-8')
            else:
                msg="None"
                msg = bytes(final_predicted_class, 'utf-8')
            conn.send(msg) 
            print(msg)
            # Reset frame counter and captured frames for the next batch
            frame_counter = 0
            captured_frames = []

        # Check for key press to exit the loop (press 'q' to exit)
        if cv2.waitKey(1) & 0xFF == ord('q'):
            break

    # Release the camera
    camera.release()
    cv2.destroyAllWindows()

