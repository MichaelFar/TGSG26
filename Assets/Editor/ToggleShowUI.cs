using UnityEditor;
using UnityEditor.ShortcutManagement;

public class ShowHideUILayer
{
    [Shortcut("Toggle UI Layer Visibility")]
    private static void ShowHideLayer()
    {
        Tools.visibleLayers ^= (1 << 5);
    }
}
