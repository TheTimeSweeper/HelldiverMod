using HellDiverMod.General.Components.UI;
using System.Collections.Generic;
using UnityEngine;

namespace HellDiverMod.Survivors.HellDiver.Components.UI {
    public class HellDiverUI : MonoBehaviour, ICompanionUI<StratagemInputController>
    {
        [SerializeField]
        private CanvasGroup stratagemContainer;
        [SerializeField]
        private Transform stratagemGrid;
        public StratagemUIEntry entryPrefab;

        private List<StratagemUIEntry> _entries = new List<StratagemUIEntry>();

        private StratagemInputController _inputController;

        private bool _firstInit;

        public void OnInitialize(StratagemInputController companionComponent)
        {
            _inputController = companionComponent;
            Show(false);
        }

        public void FirstInit()
        {
            if (_firstInit)
                return;

            int i = 0;
            for (; i < _inputController.sequences.Count; i++)
            {
                if (i >= _entries.Count)
                {
                    _entries.Add(Instantiate(entryPrefab, stratagemGrid));
                }
                _entries[i].Init(_inputController.sequences[i]);
            }
            for (; i < _entries.Count; i++)
            {
                _entries[i].Show(false);
            }
        }

        public void OnUIUpdate() { }

        public void Show(bool shouldShow)
        {
            FirstInit();
            stratagemContainer.alpha = shouldShow? 1: 0;
        }

        public void UpdateSequence(int i, bool inputSuccess, int progress)
        {
            _entries[i].UpdateInput(inputSuccess, progress);
        }

        public void UpdateComplete(int completed)
        {
            for (int i = 0; i < _entries.Count; i++)
            {
                _entries[i].UpdateComplete(i == completed);
            }
        }

        public void Reset()
        {
            for (int i = 0; i < _entries.Count; i++)
            {
                _entries[i].Reset();
            }
        }
    }
}
