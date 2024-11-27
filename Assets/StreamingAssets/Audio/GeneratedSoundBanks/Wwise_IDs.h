/////////////////////////////////////////////////////////////////////////////////////////////////////
//
// Audiokinetic Wwise generated include file. Do not edit.
//
/////////////////////////////////////////////////////////////////////////////////////////////////////

#ifndef __WWISE_IDS_H__
#define __WWISE_IDS_H__

#include <AK/SoundEngine/Common/AkTypes.h>

namespace AK
{
    namespace EVENTS
    {
        static const AkUniqueID BAT_ATTACK = 3423338245U;
        static const AkUniqueID BAT_MOVEMENT = 2757895098U;
        static const AkUniqueID CHECKPOINT = 612075679U;
        static const AkUniqueID CHEST_OPENS = 2787926982U;
        static const AkUniqueID COFFIN = 2274969374U;
        static const AkUniqueID DESTROY = 3936390293U;
        static const AkUniqueID DIG = 445985471U;
        static const AkUniqueID DISEMBODY = 1029929321U;
        static const AkUniqueID DOOR_OPENS = 3875653779U;
        static const AkUniqueID DOOR_OPENS_STOP = 2824641886U;
        static const AkUniqueID EMBODY = 3989366099U;
        static const AkUniqueID ENEMY_ALERT = 493036092U;
        static const AkUniqueID EVIL_DIALOGUE = 1316490268U;
        static const AkUniqueID FOOTSTEPS = 2385628198U;
        static const AkUniqueID GOBLIN_ATTACK = 2534371823U;
        static const AkUniqueID HURT = 3193947170U;
        static const AkUniqueID HURT_ENEMY = 2758806427U;
        static const AkUniqueID ITEM_DROP = 2177967876U;
        static const AkUniqueID ITEM_PICK = 4159556712U;
        static const AkUniqueID LEVER = 2782712987U;
        static const AkUniqueID MENU_CURSOR_BACK = 2485905105U;
        static const AkUniqueID MENU_CURSOR_MOVE = 1306883239U;
        static const AkUniqueID MENU_CURSOR_SELECT = 3200708208U;
        static const AkUniqueID NORMAL_DIALOGUE = 365141489U;
        static const AkUniqueID PATH_CORRECT_01 = 4076456351U;
        static const AkUniqueID PATH_CORRECT_02 = 4076456348U;
        static const AkUniqueID PATH_CORRECT_03 = 4076456349U;
        static const AkUniqueID PATH_CORRECT_04 = 4076456346U;
        static const AkUniqueID PATH_WRONG = 3587245554U;
        static const AkUniqueID PLATE = 282449107U;
        static const AkUniqueID PROJECTILE_ARROW = 3456241342U;
        static const AkUniqueID PROJECTILE_FIRE = 3777725015U;
        static const AkUniqueID PROJECTILE_SKELETON = 3920484766U;
        static const AkUniqueID PUZZLE_SUCCESS = 2687737065U;
        static const AkUniqueID ROCK_PUSH = 2827948781U;
        static const AkUniqueID SKELETON_ATTACK = 2888548111U;
        static const AkUniqueID SPIKE_TRAP = 17253085U;
        static const AkUniqueID TESTINGEVENT = 946158365U;
        static const AkUniqueID TESTINGSTINGERS = 2550066322U;
        static const AkUniqueID WIRE_TURN = 2855283372U;
        static const AkUniqueID WOLF_ATTACK = 1571748476U;
    } // namespace EVENTS

    namespace STATES
    {
        namespace GAMESTATUS
        {
            static const AkUniqueID GROUP = 1045871717U;

            namespace STATE
            {
                static const AkUniqueID INGAME = 984691642U;
                static const AkUniqueID INMENU = 3374585465U;
                static const AkUniqueID NONE = 748895195U;
            } // namespace STATE
        } // namespace GAMESTATUS

        namespace MUSICSTATE
        {
            static const AkUniqueID GROUP = 1021618141U;

            namespace STATE
            {
                static const AkUniqueID BOSS = 1560169506U;
                static const AkUniqueID DUNGEON = 608898761U;
                static const AkUniqueID NONE = 748895195U;
                static const AkUniqueID TUTORIAL = 3762955427U;
                static const AkUniqueID VILLAGE = 3945572659U;
            } // namespace STATE
        } // namespace MUSICSTATE

        namespace PLAYERMOVEMENT
        {
            static const AkUniqueID GROUP = 1350496757U;

            namespace STATE
            {
                static const AkUniqueID MOVING = 2649703675U;
                static const AkUniqueID NONE = 748895195U;
                static const AkUniqueID STATIC = 1409504247U;
            } // namespace STATE
        } // namespace PLAYERMOVEMENT

        namespace PLAYERSTATUS
        {
            static const AkUniqueID GROUP = 3800848640U;

            namespace STATE
            {
                static const AkUniqueID ALIVE = 655265632U;
                static const AkUniqueID DEAD = 2044049779U;
                static const AkUniqueID NONE = 748895195U;
            } // namespace STATE
        } // namespace PLAYERSTATUS

    } // namespace STATES

    namespace SWITCHES
    {
        namespace FOOTSTEPS_STATUS
        {
            static const AkUniqueID GROUP = 2332653019U;

            namespace SWITCH
            {
                static const AkUniqueID FLYING = 3827174396U;
                static const AkUniqueID WALKING = 340271938U;
            } // namespace SWITCH
        } // namespace FOOTSTEPS_STATUS

        namespace PLAYER_MOVEMENT
        {
            static const AkUniqueID GROUP = 541470702U;

            namespace SWITCH
            {
                static const AkUniqueID MOVING = 2649703675U;
                static const AkUniqueID STATIC = 1409504247U;
            } // namespace SWITCH
        } // namespace PLAYER_MOVEMENT

        namespace TERRAIN
        {
            static const AkUniqueID GROUP = 354267144U;

            namespace SWITCH
            {
                static const AkUniqueID DIRT = 2195636714U;
                static const AkUniqueID DUNGEON = 608898761U;
                static const AkUniqueID FLYING = 3827174396U;
                static const AkUniqueID GRASS = 4248645337U;
                static const AkUniqueID STONE = 1216965916U;
            } // namespace SWITCH
        } // namespace TERRAIN

    } // namespace SWITCHES

    namespace GAME_PARAMETERS
    {
        static const AkUniqueID FLAMES_VOLUME = 3035815762U;
        static const AkUniqueID MASTER = 4056684167U;
        static const AkUniqueID MUSIC = 3991942870U;
        static const AkUniqueID PATH_PITCH = 1446300075U;
        static const AkUniqueID PLAYER_HEALTH = 215992295U;
        static const AkUniqueID SFX = 393239870U;
    } // namespace GAME_PARAMETERS

    namespace TRIGGERS
    {
        static const AkUniqueID CHECKPOINT = 612075679U;
        static const AkUniqueID PATH_CORRECT = 4156519531U;
        static const AkUniqueID PATH_WRONG = 3587245554U;
        static const AkUniqueID PUZZLE_SUCCESS = 2687737065U;
        static const AkUniqueID RESUMEMUSIC = 2170724709U;
        static const AkUniqueID STOPMUSIC = 1917263390U;
    } // namespace TRIGGERS

    namespace BANKS
    {
        static const AkUniqueID INIT = 1355168291U;
        static const AkUniqueID MAIN = 3161908922U;
        static const AkUniqueID MUSIC = 3991942870U;
        static const AkUniqueID TESTING = 1512859615U;
        static const AkUniqueID TITLE = 3705726509U;
    } // namespace BANKS

    namespace BUSSES
    {
        static const AkUniqueID AMBIANCEMASTER = 1436777591U;
        static const AkUniqueID CHARACTERS = 1557941045U;
        static const AkUniqueID ENVIRONMENTMASTER = 3802034858U;
        static const AkUniqueID MASTER_AUDIO_BUS = 3803692087U;
        static const AkUniqueID MUSICMASTER = 3199789248U;
        static const AkUniqueID NPCCREATURESMASTER = 3097786830U;
        static const AkUniqueID OBJECTSMASTER = 3123271961U;
        static const AkUniqueID PLAYERMASTER = 3538689948U;
        static const AkUniqueID SFXMASTER = 1681633080U;
        static const AkUniqueID UIMASTER = 1992125169U;
    } // namespace BUSSES

    namespace AUX_BUSSES
    {
        static const AkUniqueID DUNGEON = 608898761U;
        static const AkUniqueID OVERWORLD = 1562068129U;
        static const AkUniqueID REVERBS = 3545700988U;
    } // namespace AUX_BUSSES

    namespace AUDIO_DEVICES
    {
        static const AkUniqueID NO_OUTPUT = 2317455096U;
        static const AkUniqueID SYSTEM = 3859886410U;
    } // namespace AUDIO_DEVICES

}// namespace AK

#endif // __WWISE_IDS_H__
