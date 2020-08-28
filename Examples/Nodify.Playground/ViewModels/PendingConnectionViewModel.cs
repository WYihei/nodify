﻿namespace Nodify.Playground
{
    public class PendingConnectionViewModel : ObservableObject
    {
        private NodifyEditorViewModel _graph = default!;
        public NodifyEditorViewModel Graph
        {
            get => _graph;
            internal set => SetProperty(ref _graph, value);
        }

        private ConnectorViewModel? _source;
        public ConnectorViewModel? Source
        {
            get => _source;
            set => SetProperty(ref _source, value);
        }

        // Could be a connector, a node or the editor
        private object? _target;
        public object? Target
        {
            get => _target;
            set => SetProperty(ref _target, value);
        }

        private object? _previewTarget;
        public object? PreviewTarget
        {
            get => _previewTarget;
            set
            {
                if (SetProperty(ref _previewTarget, value))
                {
                    OnPreviewTargetChanged();
                }
            }
        }

        private string _previewText = "Place node";
        public string PreviewText
        {
            get => _previewText;
            set => SetProperty(ref _previewText, value);
        }

        protected virtual void OnPreviewTargetChanged()
        {
            bool canConnect = PreviewTarget != null && Graph.Schema.CanAddConnection(Source!, PreviewTarget);
            PreviewText = PreviewTarget switch
            {
                ConnectorViewModel con => $"{(canConnect ? "Connect" : "Can't connect")} to {con.Title ?? "pin"}",
                FlowNodeViewModel flow => $"{(canConnect ? "Connect" : "Can't connect")} to {flow.Title ?? "node"}",
                _ => $"Place node"
            };
        }
    }
}
