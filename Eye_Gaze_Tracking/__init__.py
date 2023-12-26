"""
Demonstration of the GazeTracking library.
Check the README.md for complete documentation.
"""
import cv2
import time
import socket
import pickle
from gaze_tracking import GazeTracking
"""
Demonstration of the GazeTracking library.
Check the README.md for complete documentation.
"""

host = '127.0.0.1' 
port = 4343
server_socket = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
server_socket.bind((host, port))
server_socket.listen(1)
print(f"Server is listening on {host}:{port}")

client_socket, client_address = server_socket.accept()
print(f"Connection from {client_address} established.")

start_time = time.time()
max_duration = 0
current_duration = 0
max_direction = None
max_position = None

gaze = GazeTracking()
webcam = cv2.VideoCapture(0)

while True:
    # We get a new frame from the webcam
    _, frame = webcam.read()

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

        
    cv2.putText(frame, text, (90, 60), cv2.FONT_HERSHEY_DUPLEX, 1.6, (147, 58, 31), 2)
 
    left_pupil = gaze.pupil_left_coords()
    right_pupil = gaze.pupil_right_coords()
    #cv2.putText(frame, "Left pupil:  " + str(left_pupil), (90, 130), cv2.FONT_HERSHEY_DUPLEX, 0.9, (147, 58, 31), 1)
    #cv2.putText(frame, "Right pupil: " + str(right_pupil), (90, 165), cv2.FONT_HERSHEY_DUPLEX, 0.9, (147, 58, 31), 1)

    cv2.imshow("Demo", frame)
    time.sleep(1)

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
client_socket.sendall(serialized_data)
client_socket.close()
server_socket.close()
webcam.release()
cv2.destroyAllWindows()

