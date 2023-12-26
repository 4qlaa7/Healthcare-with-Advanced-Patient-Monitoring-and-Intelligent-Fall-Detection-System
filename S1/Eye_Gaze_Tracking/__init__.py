

import cv2
import time
import pickle
from Eye_Gaze_Tracking.gaze_tracking import GazeTracking




def eyegaze(webcam):
    start_time = time.time()
    max_duration = 0
    current_duration = 0
    max_direction = None
    max_position = None

    gaze = GazeTracking()
    

    while True:
        # We get a new frame from the webcam
        ret, frame = webcam.read()

        if ret:
            # We send this frame to GazeTracking to analyze it
            gaze.refresh(frame)

            frame = gaze.annotated_frame()
            text = ""
            
            if gaze.is_right():
                text = "Looking right"
                current_duration = time.time() - start_time
                current_position = gaze.pupil_right_coords() if gaze.pupil_right_coords() else gaze.pupil_left_coords()
            elif gaze.is_left():
                text = "Looking left"
                current_duration = time.time() - start_time
                current_position = gaze.pupil_left_coords() if gaze.pupil_left_coords() else gaze.pupil_right_coords()


            elif gaze.is_center():
                text = "Looking center"
                current_duration = time.time() - start_time
                current_position = gaze.pupil_left_coords() if gaze.pupil_left_coords() else gaze.pupil_right_coords()

                
            if current_duration > max_duration:
                max_duration = current_duration
                max_direction = text
                max_position = current_position

                
            #cv2.putText(frame, text, (90, 60), cv2.FONT_HERSHEY_DUPLEX, 1.6, (147, 58, 31), 2)
        
            left_pupil = gaze.pupil_left_coords()
            right_pupil = gaze.pupil_right_coords()
            #cv2.putText(frame, "Left pupil:  " + str(left_pupil), (90, 130), cv2.FONT_HERSHEY_DUPLEX, 0.9, (147, 58, 31), 1)
            #cv2.putText(frame, "Right pupil: " + str(right_pupil), (90, 165), cv2.FONT_HERSHEY_DUPLEX, 0.9, (147, 58, 31), 1)

            #cv2.imshow("Demo", frame)
            time.sleep(1)
            print(max_duration)
            print("Maximum Duration: {:.2f} s ({}) at X: {:.2f}, Y: {:.2f}".format(
                max_duration, max_direction, max_position[0], max_position[1]))
            max_data = {
                        max_duration,
                        max_direction,
                        max_position}  
            if cv2.waitKey(1) == 27:
                print("Maximum Duration: {:.2f} s ({}) at X: {:.2f}, Y: {:.2f}".format(
                max_duration, max_direction, max_position[0], max_position[1]))
                max_data = {
                            max_duration,
                            max_direction,
                            max_position
                        }  
                break
            serialized_data = pickle.dumps(max_data)
            print(max_data)
            return max_position
            
            
    