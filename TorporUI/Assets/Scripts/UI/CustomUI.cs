using UnityEngine;


namespace Custom.UI
{
    [ExecuteInEditMode()]
    public class CustomUI : MonoBehaviour
    {
        public ButtonSkinData skinData;
        public bool _isDirty;

        protected virtual void OnSkinUI()
        {
            _isDirty = true;
        }

        public virtual void Awake()
        {

        }

        public virtual void Update()
        {
            if(!_isDirty) return;
            OnSkinUI();
        }

        private void OnValidate ()
        {
            _isDirty = true;
        }
    }
}

