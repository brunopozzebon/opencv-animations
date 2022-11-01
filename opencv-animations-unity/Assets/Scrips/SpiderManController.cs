using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using DefaultNamespace;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.UI;

public class SpiderManController : MonoBehaviour
{
    private List<Armature> armature;
    private int index;

    public LineRenderer lineRendererLegRight;
    public LineRenderer lineRendererLegLeft;
    public LineRenderer lineRendererArmRight;
    public LineRenderer lineRendererArmLeft;
    public LineRenderer lineRendererTrunk;

    public Button animation1Button;
    public Button animation2Button;
    public Button animation3Button;
    
    
    private GameObject leftShoulderGO,
        leftElbowGO,
        leftLegGO,
        leftKneeGO,
        rightShoulderGO,
        rightElbowGO,
        rightLegGO,
        rightKneeGO,
        headGO,
        neckGO,
        wristGO;
    
    private bool canRead = true;

    private void Start()
    {
        leftShoulderGO = GameObject.Find("leftShoulder");
        leftElbowGO = GameObject.Find("leftElbow");
        leftLegGO = GameObject.Find("leftLeg");
        leftKneeGO = GameObject.Find("leftKnee");

        rightShoulderGO = GameObject.Find("rightShoulder");
        rightElbowGO = GameObject.Find("rightElbow");
        rightLegGO = GameObject.Find("rightLeg");
        rightKneeGO = GameObject.Find("rightKnee");

        wristGO = GameObject.Find("wrist");
        neckGO = GameObject.Find("neck");
        headGO = GameObject.Find("head");
        
        animation1Button.onClick.AddListener(() => { setArmature(0);});
        animation2Button.onClick.AddListener(() => { setArmature(1);});
        animation3Button.onClick.AddListener(() => { setArmature(2);});

        setArmature(0);

        StartCoroutine(animationCourotine());
    }

    private void setArmature(int index)
    {
        canRead = false;
        string filename = "animation_" + (index + 1)+".json";
        string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
        string fullName = Path.Combine(desktopPath, filename);
        using (StreamReader steamReader = new StreamReader(fullName))
        {
            string content = steamReader.ReadToEnd();
            armature = JsonConvert.DeserializeObject<List<Armature>>(content);
        }

        canRead = true;
    }

    IEnumerator animationCourotine()
    {
        while (canRead)
        {
            if (armature.Count > index)
            {
                instantiateDots();
                updateArmature();

                index = (index + 1) % armature.Count;
            }

            yield return new WaitForSeconds(0.1f);
        }
    }

    public void instantiateDots()
    {
        Vec3 leftShoulder = armature[index].leftShoulder;
        Vec3 leftElbow = armature[index].leftElbow;
        Vec3 leftHand = armature[index].leftHand;
        Vec3 leftLeg = armature[index].leftLeg;
        Vec3 leftKnee = armature[index].leftKnee;
        Vec3 leftFoot = armature[index].leftFoot;

        Vec3 rightShoulder = armature[index].rightShoulder;
        Vec3 rightElbow = armature[index].rightElbow;
        Vec3 rightHand = armature[index].rightHand;
        Vec3 rightLeg = armature[index].rightLeg;
        Vec3 rightKnee = armature[index].rightKnee;
        Vec3 rightFoot = armature[index].rightFoot;

        Vec3 wrist = armature[index].wrist;
        Vec3 neck = armature[index].neck;
        Vec3 head = armature[index].head;

        lineRendererArmLeft.SetPositions(
            new[] { leftShoulder.toVector3(), leftElbow.toVector3(), leftHand.toVector3() });
        lineRendererArmRight.SetPositions(new[]
            { rightShoulder.toVector3(), rightElbow.toVector3(), rightHand.toVector3() });
        lineRendererLegLeft.SetPositions(new[] { leftLeg.toVector3(), leftKnee.toVector3(), leftFoot.toVector3() });
        lineRendererLegRight.SetPositions(new[] { rightLeg.toVector3(), rightKnee.toVector3(), rightFoot.toVector3() });
        lineRendererTrunk.SetPositions(new[] { wrist.toVector3(), neck.toVector3(), head.toVector3() });
    }

    public void updateArmature()
    {
        leftShoulderGO.transform.localRotation = Quaternion.Euler(
            getFirstJointRotation(armature[index].leftShoulder, armature[index].leftElbow)
        );

        rightShoulderGO.transform.localRotation = Quaternion.Euler(
            getFirstJointRotation(armature[index].rightElbow, armature[index].rightShoulder)
        );

        leftElbowGO.transform.localRotation = Quaternion.Euler(
            getElbowAngle(armature[index].leftShoulder, armature[index].leftElbow,
                armature[index].leftHand, true
            )
        );

        rightElbowGO.transform.localRotation = Quaternion.Euler(
            getElbowAngle(armature[index].rightShoulder, armature[index].rightElbow,
                armature[index].rightHand, false
            )
        );

        leftLegGO.transform.localRotation = Quaternion.Euler(
            new Vector3(0, -getFirstJointRotation(
                armature[index].leftLeg, armature[index].leftKnee).z, 90f)
        );

        Vector3 leftKneeAngle = new Vector3(
            getKneeAngle(armature[index].leftLeg, armature[index].leftKnee,
                armature[index].leftFoot
            ).z, 0, 0);
        
        leftKneeGO.transform.localRotation = Quaternion.Euler(
            leftKneeAngle
        );
        
        rightLegGO.transform.localRotation = Quaternion.Euler(
            new Vector3(0, -getFirstJointRotation(
                armature[index].rightKnee, armature[index].rightLeg).z, -90f)
        );

         Vector3 rightNewAngle = new Vector3(
            getKneeAngle(armature[index].rightLeg, armature[index].rightKnee,
                armature[index].rightFoot
            ).z, 0, 0);
        
        rightKneeGO.transform.localRotation = Quaternion.Euler(
            rightNewAngle
        );
    }

    private Vector3 getFirstJointRotation(Vec3 segmentA, Vec3 segmentB)
    {
        Vec2 zViewSegmentA = new Vec2(segmentA.x, segmentA.y);
        Vec2 zViewSegmentB = new Vec2(segmentB.x, segmentB.y);

        float pointsDistance = zViewSegmentB.distanceTo(zViewSegmentA);
        float yDelta = segmentA.y - segmentB.y;

        float zRotation = Mathf.Asin(yDelta / pointsDistance);
        float zRotationInDegrees = MathUtils.toDegress(zRotation);

        return new Vector3(
            0, 0, zRotationInDegrees
        );
    }

    private Vector3 getElbowAngle(Vec3 shoulderPosition, Vec3 elbowPosition, Vec3 handPosition, bool isLeft)
    {
        Vec2 zViewSegmentA = new Vec2(shoulderPosition.x, shoulderPosition.y);
        Vec2 zViewSegmentB = new Vec2(elbowPosition.x, elbowPosition.y);
        Vec2 zViewSegmentC = new Vec2(handPosition.x, handPosition.y);

        Vec2 BA = zViewSegmentB.copy().sub(zViewSegmentA);
        Vec2 BC = zViewSegmentB.copy().sub(zViewSegmentC);
        float bcLengh = BC.length();

        float zRotation = BA.angleBetweenLine(BC);
        float zRotationInDegrees = MathUtils.toDegress(zRotation);

        if ((zViewSegmentB.copy().add(
                BA.copy().normalize().scale(bcLengh)
            ).y) < handPosition.y)
        {
            zRotationInDegrees = isLeft ? zRotationInDegrees - 180 : 180 - zRotationInDegrees;
        }
        else
        {
            zRotationInDegrees = isLeft ? 180 - zRotationInDegrees : 180 + zRotationInDegrees;
        }

        return new Vector3(
            0, 0, zRotationInDegrees
        );
    }
    
    
    private Vector3 getKneeAngle(Vec3 shoulderPosition, Vec3 elbowPosition, Vec3 handPosition)
    {
        Vec2 zViewSegmentA = new Vec2(shoulderPosition.x, shoulderPosition.y);
        Vec2 zViewSegmentB = new Vec2(elbowPosition.x, elbowPosition.y);
        Vec2 zViewSegmentC = new Vec2(handPosition.x, handPosition.y);

        Vec2 BA = zViewSegmentB.copy().sub(zViewSegmentA);
        Vec2 BC = zViewSegmentB.copy().sub(zViewSegmentC);
        float bcLengh = BC.length();

        float zRotation = BA.angleBetweenLine(BC);
        float zRotationInDegrees = MathUtils.toDegress(zRotation);

        if ((zViewSegmentB.copy().add(
                BA.copy().normalize().scale(bcLengh)
            ).y) < handPosition.y)
        {
            zRotationInDegrees = zRotationInDegrees - 180 ;
        }
        else
        {
            zRotationInDegrees =   180 + zRotationInDegrees;
        }

        return new Vector3(
            0, 0, zRotationInDegrees
        );
    }
}