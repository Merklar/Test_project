using strange.extensions.command.impl;

public class PlaySoundCommand : Command
{
    [Inject]
    public string soundName { get; private set; }

    public override void Execute()
    {
        AudioManagerFacade.PlaySound(soundName);
    }
}