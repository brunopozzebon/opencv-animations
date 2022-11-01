import os
import cv2
import mediapipe as mp

from poseCharacter import PoseCharacter

mp_pose = mp.solutions.pose
pose_video = mp_pose.Pose(static_image_mode=False, min_detection_confidence=0.7,
                          min_tracking_confidence=0.7)

mp_drawing = mp.solutions.drawing_utils





files = []
for file in os.listdir(os. getcwd()):
    if file.endswith('.mp4'):
        files.append(file)

for i in range(len(files)):
    videoFile = files[i]
    animationData = "["
    poseCharacter = None

    finalPath = f'C:\\Users\\{os.getlogin()}\\Desktop\\animation_{i+1}.json'

    if os.path.isfile(finalPath):
        os.remove(finalPath)

    file = open(finalPath, 'w')
    video = cv2.VideoCapture(videoFile)

    while video.isOpened():

        canRead, frame = video.read()
        # Flip Vertically
        frame = cv2.flip(frame, 0)
        # Flip Horizontally
        # frame = cv2.flip(frame, 1)
        if not canRead:
            break

        resultant = pose_video.process(frame)

        if resultant.pose_landmarks and mp_drawing:
            mp_drawing.draw_landmarks(image=frame, landmark_list=resultant.pose_landmarks,
                                      connections=mp_pose.POSE_CONNECTIONS,
                                      landmark_drawing_spec=mp_drawing.DrawingSpec(color=(255, 255, 255),
                                                                                   thickness=3, circle_radius=3),
                                      connection_drawing_spec=mp_drawing.DrawingSpec(color=(49, 125, 237),
                                                                                     thickness=2, circle_radius=2))

            poseCharacter = PoseCharacter(resultant.pose_landmarks.landmark)
            data = poseCharacter.getJsonData()
            animationData = animationData + data + ",\n"

        cv2.imshow("Dynamic Game", frame)
        # cv2.imwrite("test.jpeg", frame)

        if cv2.waitKey(1) & 0xFF == ord('q'):
            break

    animationData = animationData[:-2] + "]"
    file.write(animationData)
    file.close()
    video.release()
    cv2.destroyAllWindows()
