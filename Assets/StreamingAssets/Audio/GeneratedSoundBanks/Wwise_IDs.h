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
        static const AkUniqueID TESTINGEVENT = 946158365U;
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

        namespace PLAYERSTATUS
        {
            static const AkUniqueID GROUP = 3800848640U;

            namespace STATE
            {
                static const AkUniqueID ALIVE = 655265632U;
                static const AkUniqueID NONE = 748895195U;
                static const AkUniqueID STEALTH = 2909291642U;
            } // namespace STATE
        } // namespace PLAYERSTATUS

        namespace ROOMSTATE
        {
            static const AkUniqueID GROUP = 185713839U;

            namespace STATE
            {
                static const AkUniqueID DUNGEON = 608898761U;
                static const AkUniqueID NONE = 748895195U;
                static const AkUniqueID OVERWORLD = 1562068129U;
            } // namespace STATE
        } // namespace ROOMSTATE

    } // namespace STATES

    namespace SWITCHES
    {
        namespace GROUNDTEXTURES
        {
            static const AkUniqueID GROUP = 478119480U;

            namespace SWITCH
            {
                static const AkUniqueID DIRT = 2195636714U;
                static const AkUniqueID GRASS = 4248645337U;
                static const AkUniqueID STONE = 1216965916U;
                static const AkUniqueID WATER = 2654748154U;
                static const AkUniqueID WOOD = 2058049674U;
            } // namespace SWITCH
        } // namespace GROUNDTEXTURES

    } // namespace SWITCHES

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
        static const AkUniqueID ENVIRONMENTMASTER = 3802034858U;
        static const AkUniqueID MASTER_AUDIO_BUS = 3803692087U;
        static const AkUniqueID MUSICMASTER = 3199789248U;
        static const AkUniqueID NPCMASTER = 2033911932U;
        static const AkUniqueID PLAYERMASTER = 3538689948U;
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
