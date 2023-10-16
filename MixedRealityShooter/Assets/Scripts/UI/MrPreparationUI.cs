using TMPro;
using UnityEngine;

namespace UI
{
    public class MrPreparationUI : MonoBehaviour
    {
        [SerializeField] private TMP_Text _buildModeName;

        public void ChangeBuildModeName(bool isBuildMode)
        {
            if(_buildModeName == null) return;
            _buildModeName.text = isBuildMode ? "BuildMode: Place Object" : "BuildMode: Delete Object";
        }
    }
}
