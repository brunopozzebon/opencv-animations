import json

from vector import Vector


class PoseCharacter:
    def __init__(self, data):
        increment = 15
        incrementZ = 1.5
        self.leftShoulder = Vector(data[11].x * increment, (1 - data[11].y) * increment, data[11].z * incrementZ)
        self.leftElbow = Vector(data[13].x * increment, (1 - data[13].y) * increment, data[13].z * incrementZ)
        self.leftHand = Vector(data[15].x * increment, (1 - data[15].y) * increment, data[15].z * incrementZ)
        self.rightShoulder = Vector(data[12].x * increment, (1 - data[12].y) * increment, data[12].z * incrementZ)
        self.rightElbow = Vector(data[14].x * increment, (1 - data[14].y) * increment, data[14].z * incrementZ)
        self.rightHand = Vector(data[16].x * increment, (1 - data[16].y) * increment, data[16].z * incrementZ)
        self.leftLeg = Vector(data[23].x * increment, (1 - data[23].y) * increment, data[23].z * incrementZ)
        self.leftKnee = Vector(data[25].x * increment, (1 - data[25].y) * increment, data[25].z * incrementZ)
        self.leftFoot = Vector(data[27].x * increment, (1 - data[27].y) * increment, data[27].z * incrementZ)
        self.rightLeg = Vector(data[24].x * increment, (1 - data[24].y) * increment, data[24].z * incrementZ)
        self.rightKnee = Vector(data[26].x * increment, (1 - data[26].y) * increment, data[26].z * incrementZ)
        self.rightFoot = Vector(data[28].x * increment, (1 - data[28].y) * increment, data[28].z * incrementZ)
        self.wrist = self.rightLeg.copy().add(self.leftLeg).div(2)
        self.neck = self.rightShoulder.copy().add(self.leftShoulder).div(2)
        self.head = Vector(data[0].x * increment, (1 - data[0].y) * increment, data[0].z * incrementZ)

    def getJsonData(self):
        return f'{{' \
               f'"leftShoulder":{json.dumps(self.leftShoulder, default=vars)},' \
               f'"leftElbow":{json.dumps(self.leftElbow, default=vars)},' \
               f'"leftHand":{json.dumps(self.leftHand, default=vars)},' \
               f'"rightShoulder":{json.dumps(self.rightShoulder, default=vars)},' \
               f'"rightElbow":{json.dumps(self.rightElbow, default=vars)},' \
               f'"rightHand":{json.dumps(self.rightHand, default=vars)},' \
               f'"leftLeg":{json.dumps(self.leftLeg, default=vars)},' \
               f'"leftKnee":{json.dumps(self.leftKnee, default=vars)},' \
               f'"leftFoot":{json.dumps(self.leftFoot, default=vars)},' \
               f'"rightLeg":{json.dumps(self.rightLeg, default=vars)},' \
               f'"rightKnee":{json.dumps(self.rightKnee, default=vars)},' \
               f'"rightFoot":{json.dumps(self.rightFoot, default=vars)},' \
               f'"wrist":{json.dumps(self.wrist, default=vars)},' \
               f'"neck":{json.dumps(self.neck, default=vars)},' \
               f'"head":{json.dumps(self.head, default=vars)}' \
               f'}}'
