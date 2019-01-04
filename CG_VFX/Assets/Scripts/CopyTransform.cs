using UnityEngine;

public class CopyTransform : MonoBehaviour
{

    public bool copyPosition;
    [SerializeField] bool localPos = true;
    [SerializeField]
    bool x_pos;
    [SerializeField]
    bool y_pos;
    [SerializeField]
    bool z_pos;

    public bool copyRotation;
    [SerializeField]
    bool x_rot;
    [SerializeField]
    bool y_rot;
    [SerializeField]
    bool z_rot;
    public bool copyScale;
    [SerializeField]
    bool x_sc;
    [SerializeField]
    bool y_sc;
    [SerializeField]
    bool z_sc;

    public Transform positionOrigin;
    public Transform rotationOrigin;
    public Transform scaleOrigin;

    void Update()
    {
        if (copyPosition && positionOrigin != null) { CopyPosition(); }
        if (copyRotation && rotationOrigin != null) { CopyRotation(); }
        if (copyScale && scaleOrigin != null) { CopyScale(); }
    }

    void CopyPosition()
    {
        if (localPos)
        {
            float x = transform.localPosition.x;
            float y = transform.localPosition.y;
            float z = transform.localPosition.z;
            if (x_pos) { x = positionOrigin.localPosition.x; }
            if (y_pos) { y = positionOrigin.localPosition.y; }
            if (z_pos) { z = positionOrigin.localPosition.z; }

            transform.localPosition = new Vector3(x, y, z);
        }
        else
        {
            float x = transform.position.x;
            float y = transform.position.y;
            float z = transform.position.z;
            if (x_pos) { x = positionOrigin.position.x; }
            if (y_pos) { y = positionOrigin.position.y; }
            if (z_pos) { z = positionOrigin.position.z; }

            transform.position = new Vector3(x, y, z);
        }
    }
    void CopyRotation()
    {
        float x = transform.rotation.eulerAngles.x;
        float y = transform.rotation.eulerAngles.y;
        float z = transform.rotation.eulerAngles.z;
        if (x_rot) { x = rotationOrigin.rotation.eulerAngles.x; }
        if (y_rot) { y = rotationOrigin.rotation.eulerAngles.y; }
        if (z_rot) { z = rotationOrigin.rotation.eulerAngles.z; }

        transform.rotation = Quaternion.Euler(x, y, z);
    }
    void CopyScale()
    {
        float x = transform.localScale.x;
        float y = transform.localScale.y;
        float z = transform.localScale.z;
        if (x_sc) { x = scaleOrigin.localScale.x; }
        if (y_sc) { y = scaleOrigin.localScale.y; }
        if (z_sc) { z = scaleOrigin.localScale.z; }

        transform.localScale = new Vector3(x, y, z);
    }
}
